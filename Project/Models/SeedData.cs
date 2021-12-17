using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Data;
using System;
using System.Linq;

namespace Project.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ProjectContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ProjectContext>>()))
            {
                // Look for any items.
                if (context.Item.Any())
                {
                    return;   // DB has been seeded
                }

                context.Item.AddRange(
                    new Item
                    {
                        Title = "Adjectives",
                        Tag = "Adjectives",
                        Link = "https://dictionary.cambridge.org/grammar/british-grammar/adjectives_2",
                    },

                    new Item
                    {
                        Title = "Adjectives: forms",
                        Tag = "Adjectives",
                        Link = "https://dictionary.cambridge.org/grammar/british-grammar/adjectives-forms",
                    }

                );
                context.SaveChanges();
            }
        }
    }
}