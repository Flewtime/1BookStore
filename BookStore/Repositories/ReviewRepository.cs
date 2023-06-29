using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Factories;

namespace BookStore.Repositories
{
    public class ReviewRepository : IDisposable
    {
        private Database1Entities1 db;
        private Review review;

        public ReviewRepository()
        {
            db = Database.getDb();
            review = new Review();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void insertReview(double ReviewRating, string ReviewComment, DateTime ReviewDateTime, int UserID, int BookID)
        {
            review = ReviewFactory.createReview(ReviewRating, ReviewComment, ReviewDateTime, UserID, BookID);
            db.Reviews.Add(review);
            db.SaveChanges();
        }

        public void deleteReview(int ReviewID)
        {
            review = findReviewByID(ReviewID);
            if (review != null)
            {
                db.Reviews.Remove(review);
                db.SaveChanges();
            }
        }

        public void deleteAllReviewByBook(int BookID)
        {
            List<Review> list = getAllReviewByBook(BookID);
            if (list.Any())
            {
                foreach (Review r in list)
                {
                    deleteReview(r.ReviewID);
                }
            }
        }

        public void deleteAllReviewByUser(int UserID)
        {
            List<Review> list = getAllReviewByUser(UserID);
            if (list.Any())
            {
                foreach (Review r in list)
                {
                    deleteReview(r.ReviewID);
                }
            }
        }

        public void updateReview(int ReviewID, double ReviewRating, string ReviewComment, DateTime ReviewDateTime, int UserID, int BookID)
        {
            review = findReviewByID(ReviewID);
            if (review != null)
            {
                review.ReviewRating = ReviewRating;
                review.ReviewComment = ReviewComment;
                review.ReviewDateTime = ReviewDateTime;
                review.UserID = UserID;
                review.BookID = BookID;
                db.SaveChanges();
            }
        }

        public Review findReviewByID(int ReviewID)
        {
            review = db.Reviews.Find(ReviewID);
            return review;
        }

        public List<Review> getAllReview()
        {
            List<Review> list = new List<Review>();
            list = (from r in db.Reviews select r).ToList();
            return list;
        }

        public List<Review> getAllReviewByUser(int UserID)
        {
            List<Review> list = new List<Review>();
            list = (from r in db.Reviews where r.UserID == UserID select r).ToList();
            return list;
        }

        public List<Review> getAllReviewByBook(int BookID)
        {
            List<Review> list = new List<Review>();
            list = (from r in db.Reviews where r.BookID == BookID select r).ToList();
            return list;
        }

        public BookRating getAverageRatingByBook(int BookID)
        {
            BookRating bookRating = new BookRating();
            double average = 0;
            List<Review> list = getAllReviewByBook(BookID);
            if (list.Any())
            {
                foreach (Review r in list)
                {
                    average += r.ReviewRating;
                }
                average = average / list.Count;
            }

            bookRating.AverageRating = Math.Round(average, 1);
            return bookRating;
        }
    }
}