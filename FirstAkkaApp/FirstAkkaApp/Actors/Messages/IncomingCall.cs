namespace FirstAkkaApp.Actors.Messages
{
    public class IncomingCall
    {
        public IncomingCall(int phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public int PhoneNumber { get; set; }
    }
}
