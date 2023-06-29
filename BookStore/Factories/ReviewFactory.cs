using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;

namespace BookStore.Factories
{
    public class ReviewFactory
    {

        public static Review createReview(double ReviewRating, string ReviewComment, DateTime ReviewDateTime, int UserID, int BookID)
        {
            Review review = new Review();
            review.ReviewRating = ReviewRating;
            review.ReviewComment = ReviewComment;
            review.ReviewDateTime = ReviewDateTime;
            review.UserID = UserID;
            review.BookID = BookID;
            return review;
        }
    }
}