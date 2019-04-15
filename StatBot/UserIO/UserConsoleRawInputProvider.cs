using System;

namespace StatBot.UserIO
{
    public class UserConsoleRawInputProvider : IUserRawInputProvider
    {
        public string WaitUserRawInput()
        {
            return Console.ReadLine();
        }
    }
}