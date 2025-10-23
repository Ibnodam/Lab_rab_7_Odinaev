// ¡≈«Œ“ ¿«ÕŒ –¿¡Œ“¿≈“!!
//using Gr.Dao;
//using Gr.Data;
//using Gr.GraphQL;
//using Gr.GraphQL.InputTypes;
//using Gr.Models;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);
//builder.Configuration.AddJsonFile("appsettings.json", optional: false);

//builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//builder.Services
//    .AddGraphQLServer()
//    .AddQueryType<Query>()
//    .AddMutationType<Mutation>()
//    .AddType<AddTrainInputType>()
//    .AddType<UpdateTrainInputType>()
//    .AddType<AddPassengerInputType>()
//    .AddType<UpdatePassengerInputType>()
//    .AddType<AddWagonInputType>()
//    .AddType<UpdateWagonInputType>()
//    .AddType<AddTicketInputType>()
//    .AddType<UpdateTicketInputType>()
//    .AddType<AddSellerInputType>()
//    .AddType<UpdateSellerInputType>()
//    .AddProjections()
//    .AddFiltering()
//    .AddSorting();

//var app = builder.Build();

////using (var scope = app.Services.CreateScope())
////{
////    try
////    {
////        var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
////        await using var db = await factory.CreateDbContextAsync();
////        await db.Database.EnsureCreatedAsync();
////        await DataSeeder.SeedAsync(db);
////    }
////    catch (Exception ex)
////    {
////        Console.WriteLine($"Error during seeding: {ex.Message}");
////    }
////}

//app.MapGraphQL();

//app.Run();
// ¡≈«Œ“ ¿«ÕŒ –¿¡Œ“¿≈“!!


using Gr.Data;
using Gr.Dao;
using Gr.GraphQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- InMemory Database ---
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("TicketSystemInMemory"));

// --- Repositories ---
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// --- GraphQL ---
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

// --- NO SEEDING - ‰‡ÌÌ˚Â ÛÊÂ ÂÒÚ¸ ---

// --- GraphQL endpoint ---
app.MapGraphQL();

app.Run();