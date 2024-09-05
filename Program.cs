class Movie
{
    private static int _id = 0;
    public int Id { get; set; }
    public string Title { get; set; }

    public Movie(string title)
    {
        Title = title;
        Id = _id++;
    }
}


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Register the list with the Dependency Injection Containter
        builder.Services.AddSingleton<List<Movie>>();
        var app = builder.Build();

        // READ: Get all movies
        app.MapGet("/movies", (List<Movie> movies) => movies);
        //CREATE: Adds a new movie
        app.MapPost("/movies", (Movie? movie, List<Movie> movies) =>
        {
            if (movie == null)
            {
                return Results.BadRequest();
            }
            movies.Add(movie);

            return Results.Created();
        });

        // DELETE: Delete a movie with id
        app.MapDelete("/movies/{id}", (int Id) => $"Delete movie with id: {Id}");
        // UPDATE: Update a movie with id
        app.MapPut("/movies/{id}", (int Id) => $"Update movie with id: {Id}");

        app.MapGet("/health", () => "System healthy");

        app.Run();
    }
}