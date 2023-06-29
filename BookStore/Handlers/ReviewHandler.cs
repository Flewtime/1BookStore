using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using BookStore.Model;
using BookStore.Repositories;

namespace BookStore.Handlers
{
    public class ReviewHandler
    {
        private ReviewRepository reviewRepository;

        public ReviewHandler()
        {
            reviewRepository = new ReviewRepository();
        }

        public void insertReview(double ReviewRating, string ReviewComment, DateTime ReviewDateTime, int UserID, int BookID)
        {
            reviewRepository.insertReview(ReviewRating, ReviewComment, ReviewDateTime, UserID, BookID);
        }

        public void deleteReview(int ReviewID)
        {
            reviewRepository.deleteReview(ReviewID);
        }

        public void updateReview(int ReviewID, double ReviewRating, string ReviewComment, DateTime ReviewDateTime, int UserID, int BookID)
        {
            reviewRepository.updateReview(ReviewID, ReviewRating, ReviewComment, ReviewDateTime, UserID, BookID);
        }

        public Review findReviewByID(int ReviewID)
        {
            return reviewRepository.findReviewByID(ReviewID);
        }

        public List<Review> getAllReview()
        {
            return reviewRepository.getAllReview();
        }

        public List<Review> getAllReviewByUser(int UserID)
        {
            return reviewRepository.getAllReviewByUser(UserID);
        }

        public List<Review> getAllReviewByBook(int BookID)
        {
            return reviewRepository.getAllReviewByBook(BookID);
        }

        public BookRating getAverageRatingByBook(int BookID)
        {
            return reviewRepository.getAverageRatingByBook(BookID);
        }
    }
}