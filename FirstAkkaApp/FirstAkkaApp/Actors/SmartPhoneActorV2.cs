using Akka.Actor;
using FirstAkkaApp.Actors.Messages;
using System;

namespace FirstAkkaApp.Actors
{
    public class SmartPhoneActorV2 : ReceiveActor
    {
        private int _phoneNumber = 0;
        private int _lostMessage = 0;

        public SmartPhoneActorV2()
        {
            Receiver();
        }

        private void Receiver()
        {
            Receive<SmsMessage>(message =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"SmartPhoneActor New SMS received: {message.Text}");

                _lostMessage = 0;
            });

            Receive<IncomingCall>(message => {
                _phoneNumber = message.PhoneNumber;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"SmartPhoneActor New Incoming Call received: {_phoneNumber}");

                Become(Busy);

            });
        }

        private void Busy()
        {
            Receive<IncomingCall>(message => {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"SmartPhoneActor Is busy with other call sorry....");
            });

            Receive<SmsMessage>(message =>
            {
                _lostMessage++;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"SmartPhoneActor queued messages: {_lostMessage}");
            });

            Receive<HangUp>(message =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"SmartPhoneActor Ended Call with: {_phoneNumber}");
                _phoneNumber = 0;
                Become(Receiver);
            });
        }
    }
}
