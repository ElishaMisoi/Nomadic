using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Nomadic.Helpers
{
    public static class DialogsHelper
    {
        public enum Errors
        {
            NetworkError,
            Defined,
            UndefinedError,
            InputError
        }

        static CancellationTokenSource cts;

        public static void CancelActionSheet()
        {
            if (cts?.IsCancellationRequested ?? true)
                return;

            cts.Cancel();
        }

        static readonly string UndefinedError = "Something went wrong, please try again later.";
        static readonly string NetworkError = "Network Error.";
        static readonly string InputError = " is required.";

        public static void HandleDialogMessage(Errors error, string message = "")
        {
            switch (error)
            {
                case Errors.NetworkError:
                    message = "    " + NetworkError + "    ";
                    break;
                case Errors.UndefinedError:
                    message = "    " + UndefinedError + "    ";
                    break;
                case Errors.Defined:
                    message = "    " + message + "    ";
                    break;
                case Errors.InputError:
                    message = "    " + message + InputError + "    ";
                    break;
            }
            UserDialogs.Instance.Toast(new ToastConfig(message)
            .SetBackgroundColor(Color.FromHex("#333333"))
            .SetMessageTextColor(Color.White)
            .SetDuration(TimeSpan.FromSeconds(3))
            .SetPosition(ToastPosition.Bottom)
            );
        }

        public static IProgressDialog ProgressDialog = UserDialogs.Instance.Progress(new ProgressDialogConfig
        {
            AutoShow = false,
            CancelText = "Cancel",
            IsDeterministic = false,
            MaskType = MaskType.Black,
            Title = null
        });

        public static async Task OpenBrowser(string url)
        {
            await Browser.OpenAsync(url, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show
            });
        }

        public static async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
            });
        }

        public static async Task SendEmail(string subject, string body, List<string> recipients)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
