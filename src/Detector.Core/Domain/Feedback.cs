namespace Detector.Core.Domain
{
    public class Feedback
    {
        public int Incorrect { get; set; }
        public int NotFound { get; set; }
        public int MultipleFound { get; set; }
        public int IncorrectBox { get; set; }

        public Feedback(int incorrect, int notFound, int multipleFound, int incorrectBox)
        {
            Incorrect = incorrect;
            NotFound = notFound;
            MultipleFound = multipleFound;
            IncorrectBox = incorrectBox;
        }

    }
}