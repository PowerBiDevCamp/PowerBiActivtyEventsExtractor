using System;
using PowerBiActivtyEventsExtractor.Models;

namespace PowerBiActivtyEventsExtractor {
  class Program {
    static void Main() {

      DateTime date = new DateTime(2021, 8, 16);
      ActivityEventExtractor.GetActivityEvents(date);

    }
  }
}
