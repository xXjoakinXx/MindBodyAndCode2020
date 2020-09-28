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
            string input = null;
            do
            {
                Console.WriteLine("EJEMPLOS DE AKKA.NET!!");
                Console.WriteLine("1 - Ejemplo simple de SmartPhoneActor. (Simple recepción de mensajes)");
                Console.WriteLine("2 - Extender SmartPhoneActor con cambio de estado. (Cambia el comportamiento al recibir mensajes)");
                Console.WriteLine("'exit' - para salir");
                Console.WriteLine("Introduce el número de ejemplo a ejectuar o 'exit' para salir....");

                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        FirstAkkaExample();
                        break;
                    case "2":
                        StateChangeExample();
                        break;
                }

                Console.WriteLine();
            } while (input != "exit");

           

        }

        private static void FirstAkkaExample()
        {
            // Instanciamos un sistema de actores
            var actorSystem = ActorSystem.Create("SmartPhoneActorSystem");

            // Desplegamos en el sistema de actores un nuevo Actor y nos quedamos con su referencia
            var smartPhoneActor = actorSystem.ActorOf<SmartPhoneActorV1>("smartPhoneActorOne");

            smartPhoneActor.Tell(new LostCallMessage());
            smartPhoneActor.Tell(new LostCallMessage());
            smartPhoneActor.Tell(new LostCallMessage());
            var response = smartPhoneActor.Ask<SmsMessage>(new SmsMessage("Hola! Te he llamado pero no respondías... espero que estés bien!"));

            Console.WriteLine(response.Result.Text);
        }

        private static void StateChangeExample()
        {
            // Instanciamos un sistema de actores
            var actorSystem = ActorSystem.Create("SmartPhoneActorSystem");

            // Desplegamos en el sistema de actores un nuevo Actor y nos quedamos con su referencia
            var smartPhoneActor = actorSystem.ActorOf<SmartPhoneActorV2>("smartPhoneActorTwo");

            smartPhoneActor.Tell(new SmsMessage("Ey! Cuanto tiempo! ahora en un rato te llamare"));
            smartPhoneActor.Tell(new IncomingCall(664534231));
            smartPhoneActor.Tell(new SmsMessage("Su pedido de Amazon ha sido entregado!"));
            smartPhoneActor.Tell(new IncomingCall(667584934));
            smartPhoneActor.Tell(new SmsMessage("El mensajero no pudo realizar la entrega"));
            smartPhoneActor.Tell(new HangUp());
            smartPhoneActor.Tell(new SmsMessage("Me ha gustado mucho hablar contigo, seguiremos en contacto!"));

            Console.ReadLine();
        }
    }
}
