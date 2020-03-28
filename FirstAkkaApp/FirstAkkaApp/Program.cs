using Akka.Actor;
using FirstAkkaApp.Actors;
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

            // Enviamos al actor smartPhoneActorOne un mensaje
            smartPhoneActor.Tell("Hola! ¿Que tal estas?");

            Console.ReadLine();
        }
    }
}
