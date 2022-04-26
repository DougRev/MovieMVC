using MVC.Data;
using MVC.Models.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class MovieService
    {
        

        public MovieService()
        {
            
        }

        public bool CreateMovie(MovieCreate model)
        {
            var entity = new Movie()
            { 
                Id = model.Id,
                Title = model.Title,
                Genre = model.Genre,
                ReleaseDate = model.ReleaseDate,
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Movies.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<MovieListItem> GetMovies()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Movies
                    .Select(e => new MovieListItem()
                    {
                        Id=e.Id,
                        Title = e.Title,
                        Genre = e.Genre,
                        ReleaseDate = e.ReleaseDate,
                    }
                    );
                return query.ToArray();
            }
        }

        public MovieDetails GetMovieById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Movies
                    .Single(e => e.Id == id);
                return new MovieDetails
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Genre = entity.Genre,
                    ReleaseDate = entity.ReleaseDate,
                };
            }
        }

        public bool EditMovie(MovieEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Movies
                    .Single(e => e.Id == model.Id);

                entity.Title = model.Title;
                entity.Genre = model.Genre;
                entity.ReleaseDate = model.ReleaseDate;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteMovie(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Movies
                    .Single(e => e.Id == Id);
                ctx.Movies.Remove(entity);
                return ctx.SaveChanges() == 1;

            }
        }
    }
}
