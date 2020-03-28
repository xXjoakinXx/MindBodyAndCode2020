using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAkkaApp.Actors
{
    public class SmartPhoneActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            Console.WriteLine($"SmartPhoneActor Receive a message: {message}");
        }
    }
}
