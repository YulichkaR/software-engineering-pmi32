﻿using System.Diagnostics.CodeAnalysis;
using EShop.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EShop.Presentation.Extensions;

public static class MigrateDbExtension
{
   public static void ApplyMigrations(this IApplicationBuilder app)
   {
      using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

      using ApplicationDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

      dbContext.Database.Migrate();
   }
}