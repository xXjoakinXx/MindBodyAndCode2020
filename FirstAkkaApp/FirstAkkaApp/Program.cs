using Akka.Actor;
using FirstAkkaApp.Actors;
using FirstAkkaApp.Actors.Messages;
using System;

namespace FirstAkkaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instanciamos un sistema de actores
            var actorSystem = ActorSystem.Create("SmartPhoneActorSystem");

            // Desplegamos en el sistema de actores un nuevo Actor y nos quedamos con su referencia
            var smartPhoneActor = actorSystem.ActorOf<SmartPhoneActor>("smartPhoneActorOne");

            smartPhoneActor.Tell(new LostCallMessage());
            smartPhoneActor.Tell(new LostCallMessage());
            smartPhoneActor.Tell(new LostCallMessage());
            var response = smartPhoneActor.Ask<SmsMessage>(new SmsMessage("Hola! Te he llamado pero no respondías... espero que estés bien!"));

            Console.WriteLine(response.Result.Text);

            Console.ReadLine();
        }
    }
}
