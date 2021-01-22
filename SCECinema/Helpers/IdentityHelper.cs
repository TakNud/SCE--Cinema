using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SCECinema.DAL;
using SCECinema.Models;

namespace SCECinema.Helpers
{
    public class IdentityHelper
    {
        internal static void SeedIdentities(AppDbContext context)

    {

            var userManager = new UserManager<AppUser>(new UserStore<AppUser>(context));
            
    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            



    if (!roleManager.RoleExists(RoleNames.ROLE_ADMIN))
                
    {
                
        var roleresult = roleManager.Create(new IdentityRole(RoleNames.ROLE_ADMIN));
                
    }
            
    if (!roleManager.RoleExists(RoleNames.ROLE_EMPLOYEE))
                
    {
                
        var roleresult = roleManager.Create(new IdentityRole(RoleNames.ROLE_EMPLOYEE));
                
    }
            
    if (!roleManager.RoleExists(RoleNames.ROLE_REGISTER))
                
    {
                
        var roleresult = roleManager.Create(new IdentityRole(RoleNames.ROLE_REGISTER));
                
    }


            string userName = "shmueat@ac.sce.ac.il";
            string password= "$tr0ngP@ssw0rd!";

            AppUser user = userManager.FindByName(userName);
            
            if (user == null)
               {
                
                user = new AppUser()

                {
                    
                    UserName = userName,

                        Email = userName,

                        EmailConfirmed = true,
                        LastName = "Atias",
                FirstName = "Shmulik",
                    Birthday = new DateTime(1987, 09, 29),

            };
                
    IdentityResult userResult = userManager.Create(user, password);
                
    if (userResult.Succeeded)
                    
    {
                    
        var result = userManager.AddToRole(user.Id, RoleNames.ROLE_ADMIN);
                    
    }



            }







         

        }



    }
}