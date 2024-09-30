using System;

namespace SocialMediaFactoryMethod
{

    abstract class SocialNetwork
    {
        public abstract void Login(string identifier, string password);
        public abstract void PublishMessage(string message);
    }

    class Facebook : SocialNetwork
    {
        private string _login;
        private string _password;

        public override void Login(string login, string password)
        {
            _login = login;
            _password = password;
            Console.WriteLine($"Logged in to Facebook with login: {_login}");
        }

        public override void PublishMessage(string message)
        {
            Console.WriteLine($"Publishing message to Facebook: {message}");
        }
    }

    class LinkedIn : SocialNetwork
    {
        private string _email;
        private string _password;

        public override void Login(string email, string password)
        {
            _email = email;
            _password = password;
            Console.WriteLine($"Logged in to LinkedIn with email: {_email}");
        }

        public override void PublishMessage(string message)
        {
            Console.WriteLine($"Publishing message to LinkedIn: {message}");
        }
    }

    abstract class SocialNetworkFactory
    {
        public abstract SocialNetwork CreateSocialNetwork();
    }

    class FacebookFactory : SocialNetworkFactory
    {
        public override SocialNetwork CreateSocialNetwork()
        {
            return new Facebook();
        }
    }

    class LinkedInFactory : SocialNetworkFactory
    {
        public override SocialNetwork CreateSocialNetwork()
        {
            return new LinkedIn();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Facebook:\n---------------------------------------------------");
            SocialNetworkFactory facebookFactory = new FacebookFactory();
            SocialNetwork facebook = facebookFactory.CreateSocialNetwork();
            facebook.Login("user_facebook", "password_facebook");
            facebook.PublishMessage("Facebook message");

            Console.WriteLine("\n\nLinkedin:\n---------------------------------------------------");
            SocialNetworkFactory linkedInFactory = new LinkedInFactory();
            SocialNetwork linkedIn = linkedInFactory.CreateSocialNetwork();
            linkedIn.Login("user_linkedin@example.com", "password_linkedin");
            linkedIn.PublishMessage("Linkedin message");

        }
    }
}
