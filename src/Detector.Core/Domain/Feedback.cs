namespace Detector.Core.Domain
{
    public class Feedback
    {
        public int Correct { get; set; }

        public int Incorrect { get; set; }
        public int NotFound { get; set; }
        public int MultipleFound { get; set; }
        public int IncorrectBox { get; set; }


        public Feedback(int correct, int incorrect, int notFound, int multipleFound, int incorrectBox)
        {
            Correct = correct;
            Incorrect = incorrect;
            NotFound = notFound;
            MultipleFound = multipleFound;
            IncorrectBox = incorrectBox;
        }

    }
}