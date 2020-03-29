using System;

namespace FirstAkkaApp.Actors.Messages
{
    public class SmsMessage
    {
        public SmsMessage(string text)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public string Text { get; }
    }
}
