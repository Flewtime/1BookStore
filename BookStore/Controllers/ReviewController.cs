using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Handlers;
using BookStore.Repositories;

namespace BookStore.Controllers
{
    public class ReviewController
    {
        private ReviewHandler reviewHandler;

        public ReviewController()
        {
            reviewHandler = new ReviewHandler();
        }

        public void insertReview(double ReviewRating, string ReviewComment, DateTime ReviewDateTime, int UserID, int BookID)
        {
            reviewHandler.insertReview(ReviewRating, ReviewComment, ReviewDateTime, UserID, BookID);
        }

        public void deleteReview(int ReviewID)
        {
            reviewHandler.deleteReview(ReviewID);
        }

        public void updateReview(int ReviewID, double ReviewRating, string ReviewComment, DateTime ReviewDateTime, int UserID, int BookID)
        {
            reviewHandler.updateReview(ReviewID, ReviewRating, ReviewComment, ReviewDateTime, UserID, BookID);
        }

        public Review findReviewByID(int ReviewID)
        {
            return reviewHandler.findReviewByID(ReviewID);
        }

        public List<Review> getAllReview()
        {
            return reviewHandler.getAllReview();
        }

        public List<Review> getAllReviewByUser(int UserID)
        {
            return reviewHandler.getAllReviewByUser(UserID);
        }

        public List<Review> getAllReviewByBook(int BookID)
        {
            return reviewHandler.getAllReviewByBook(BookID);
        }

        public BookRating getAverageRatingByBook(int BookID)
        {
            return reviewHandler.getAverageRatingByBook(BookID);
        }
    }
}