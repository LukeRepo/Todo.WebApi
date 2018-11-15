namespace Todo.WebApi.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Models.EF_DataBase;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Todo.WebApi.Models.EF_DataBase.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Todo.WebApi.Models.EF_DataBase.ApplicationDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "Admin",
                Email = "Admin@mymail.com",
                EmailConfirmed = true,
                Nome = "Admin",
                Cognome = "Todo",
                Level = 1,
                JoinDate = DateTime.Now
            };
            manager.Create(user, "MySuperP@ss!");


            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByName("Admin");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin" });


            var starWarsTask = new List<Lavoro>()
            {
                new Lavoro {NomeTask = "Diventare_jedi", Inserito=true, Fatto=false, Scaduto=false, Descrizione="allenarsi con Yoda", DataScadenza=DateTime.Parse("2018-10-10"), NomeBacheca = "StarWarsBacheca"  },
                new Lavoro {NomeTask = "Distrugere_Morte_Nera", Inserito=true, Fatto=false, Scaduto=false, Descrizione="pilotare x wing fino al condotto", DataScadenza=DateTime.Parse("2018-10-20"), NomeBacheca = "StarWarsBacheca"  }
            };

            var bachecaStarWars = new List<Bacheca> {
            new Bacheca
               {
                  NomeBacheca = "StarWarsBacheca",
                  listaTasks = starWarsTask
               }
            };

            var Utente = new ApplicationUser()
            {
                UserName = "User",
                Email = "User@mymail.com",
                EmailConfirmed = true,
                Nome = "User",
                Cognome = "Test",
                Level = 2,
                JoinDate = DateTime.Now,
                ListaBacheca = bachecaStarWars
            };
            manager.Create(Utente, "MySuperP@ssword!");
        }
    }
}
