﻿using PerformanceEngineering.IntruderDetection;

namespace PerformanceEngineering;

internal class Program
{
    private static void Main(string[] args)
    {
        //CutiRef();
        Console.WriteLine("Press <return> to start simulation");
        Console.ReadLine();
        var room = new Room("gallery");
        var r = new Random();

        int counter = 0;

        room.TakeMeasurements(
            () =>
            {
                ref readonly DebounceMeasurement debounce = ref room.Debounce;
                Console.WriteLine(debounce.ToString());
                ref readonly AverageMeasurement average = ref room.Average;
                Console.WriteLine(average.ToString());
                Console.WriteLine();
                counter++;
                return counter < 20000;
            });

        counter = 0;
        room.TakeMeasurements(
            () =>
            {
                // Refer to the same allocation
                ref readonly DebounceMeasurement debounce = ref room.Debounce;
                Console.WriteLine(debounce.ToString());
                ref readonly AverageMeasurement average = ref room.Average;
                Console.WriteLine(average.ToString());
                room.Intruders += (room.Intruders, r.Next(5)) switch
                {
                    (> 0, 0) => -1,
                    (< 3, 1) => 1,
                    _ => 0
                };

                Console.WriteLine(room.ToString());
                Console.WriteLine();
                counter++;
                return counter < 200000;
            });

        Console.WriteLine(room.ToString());

        Console.ReadLine();
    }

    private static void CutiRef()
    {
        int cuti = 10;
        ref int anotherCuti = ref cuti;

        Console.WriteLine($"Another cu ti {anotherCuti}");

        anotherCuti = 20;
        Console.WriteLine("The old cu ti " + cuti);

        Console.ReadLine();
    }
}