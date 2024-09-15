using Codeuctivity.ImageSharpCompare;
using Mono.Options;

namespace ImageCompare;

class Program
{
    static void Main(string[] args)
    {
        double pixelErrorTolerance = 3.0;

        bool showHelp = false;

        bool verbose = false;

        HashSet<string> alreadyCompared = new();

        OptionSet p = new()
        {
            "Usage: imgcmp [OPTIONS] FILES",
            "Image Comparison",
            "",
            "Options:",
            { "pixel-error-tolerance=", "Pixel Error Tolerance", (string v) => pixelErrorTolerance = double.Parse(v) },
            { "verbose",  "Print Debugging Messages", v => verbose = v != null },
            { "h|help",  "Show this Message and Exit", v => showHelp = v != null }
        };

        List<string> extra;

        try
        {
            extra = p.Parse(args);
        }
        catch (OptionException e)
        {
            Console.Write("imgcmp: ");
            Console.WriteLine(e.Message);
            Console.WriteLine("Try 'imgcmp --help' for more information.");

            return;
        }

        if (showHelp || extra is null || extra.Count == 0)
        {
            p.WriteOptionDescriptions(Console.Out);

            Environment.Exit(0);
        }

        foreach (string src in extra)
        {
            foreach (string cmp in extra)
            {
                if (src == cmp)
                {
                    if (verbose)
                    {
                        Console.WriteLine($"Skipping same file {src} and {cmp}");
                    }

                    continue;
                }

                // TODO: This is probably inefficient
                if (alreadyCompared.Contains($"{src}{cmp}") || alreadyCompared.Contains($"{cmp}{src}"))
                {
                    if (verbose)
                    {
                        Console.WriteLine($"Skipping already compared {src} and {cmp}");
                    }

                    continue;
                }
                else
                {
                    alreadyCompared.Add($"{src}{cmp}");
                }

                if (verbose)
                {
                    Console.WriteLine($"Comparing {src} and {cmp}");
                }

                try
                {
                    ICompareResult calcDiff = ImageSharpCompare.CalcDiff(src, cmp);

                    if (calcDiff.PixelErrorPercentage <= pixelErrorTolerance)
                    {
                        Console.WriteLine($"{src} and {cmp} match");
                    }
                }
                catch (Exception ex)
                {
                    if (verbose)
                    {
                        Console.WriteLine($"Exception during {src} comparison {cmp}: {ex}");
                    }
                }
            }
        }
    }
}
