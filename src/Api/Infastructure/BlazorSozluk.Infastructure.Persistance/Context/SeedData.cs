using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Common.Infastructure;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infastructure.Persistance.Context;

internal class SeedData
{
    private static List<User> GetUsers()
    {

        //500 Adet Sahte Bir Kullanıcı Datası Oluşturduk
        var result = new Faker<User>("tr")
            .RuleFor(x => x.Id, x => Guid.NewGuid())
            .RuleFor(x => x.CreateDate,
                    x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
            .RuleFor(x => x.FirstName, x => x.Person.FirstName)
            .RuleFor(x => x.LastName, x => x.Person.LastName)
            .RuleFor(x => x.EmailAddress, x => x.Internet.Email())
            .RuleFor(x => x.UserName, x => x.Internet.Email())
            .RuleFor(x => x.UserName, x => x.Internet.UserName())
            .RuleFor(x => x.Password, x => PasswordEncryptor.Encrypt(x.Internet.Password()))
            .RuleFor(x => x.EmailConfirmed, x => x.PickRandom(true, false))
        .Generate(500);
        return result;


    }

    public async Task SeedAsync(IConfiguration configuration)
    {
        var dbContextBuilder = new DbContextOptionsBuilder();
        dbContextBuilder.UseSqlServer(configuration["BlazorSozlukDbConnectionString"]);
        var context = new BlazorSozlukContext(dbContextBuilder.Options);
        if (context.Users.Any())
        {
            await Task.CompletedTask;
            return;
        }


        // Kullanıcıların id bilgisini userIds olarak çektik ve veritabanına ekledik
        var users = GetUsers();
        var userIds = users.Select(x => x.Id);
        await context.Users.AddRangeAsync(users);



        // Entryler için bir guid aralığı oluşturduk ve sonrasında veritabanına ekledik
        var guids = Enumerable.Range(0, 150).Select(x => Guid.NewGuid()).ToList();
        int counter = 0;
        var entries = new Faker<Entry>("tr")
                    .RuleFor(x => x.Id, x=> guids[counter++])
                    .RuleFor(x => x.CreateDate,
                        x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                    .RuleFor(x => x.Subject, x => x.Lorem.Sentence(5, 5))
                    .RuleFor(x => x.Content, x => x.Lorem.Paragraph(2))
                    .RuleFor(x => x.CreatedById, x => x.PickRandom(userIds))
                 .Generate(150);
        await context.Entries.AddRangeAsync(entries);



        // Commentler için sahte bir data oluşturup userid ve entryid değerlerini
        // yukarıdaki oluşturduğumuz guidlerden çektik ve 1000 adet comment oluşturduk
        var comments = new Faker<EntryComment>("tr")
                    .RuleFor(x => x.Id, x => Guid.NewGuid())
                    .RuleFor(x => x.CreateDate,
                        x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                    .RuleFor(x => x.Content, x => x.Lorem.Paragraph(2))
                    .RuleFor(x => x.CreatedById, x => x.PickRandom(userIds))
                    .RuleFor(x => x.EntryId, x => x.PickRandom(guids))
                .Generate(1000);
        await context.EntryComments.AddRangeAsync(comments);
        await context.SaveChangesAsync();
    }
}
