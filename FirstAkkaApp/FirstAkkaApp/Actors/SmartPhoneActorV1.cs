using Akka.Actor;
using FirstAkkaApp.Actors.Messages;
using System;

namespace FirstAkkaApp.Actors
{
    public class SmartPhoneActorV1 : ReceiveActor
    {
        private int _lostCalls = 0;

        public SmartPhoneActorV1()
        {
            Receive<SmsMessage>( message =>
            {
                Console.WriteLine($"SmartPhoneActor New SMS received: {message.Text}");
                Context.Sender.Tell(new SmsMessage("Hola! Estoy bien, me has pillado comprando en el supermercado"));
            });
            
            Receive<LostCallMessage>(message => {
                _lostCalls++;
                Console.WriteLine($"Lost call received! You have missed {_lostCalls} calls");
            });

        }
    }
}
