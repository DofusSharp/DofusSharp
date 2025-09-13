using DofusSharp.DofusDb.ApiClients;

namespace BestCrush.Domain.Models;

public record struct ProgressMessage(string Message, double? Percent, bool Done = false);

public static class AsyncProgressExtensions
{
    public static ProgressSync<ProgressMessage> DeriveSubtask(
        this ProgressSync<ProgressMessage> progress,
        double fromPercent,
        double toPercent,
        Func<ProgressMessage, string>? messageFactory = null
    ) =>
        progress.Derive(p => new ProgressMessage(messageFactory is null ? p.Message : messageFactory(p), fromPercent + (toPercent - fromPercent) * (p.Percent / 100)));

    public static ProgressSync<ProgressMessage> DeriveStep(
        this ProgressSync<ProgressMessage> progress,
        int step,
        int totalSteps,
        Func<ProgressMessage, string>? messageFactory = null
    ) =>
        progress.DeriveSubtask(100.0 * step / totalSteps, 100.0 * (step + 1) / totalSteps, messageFactory);

    public static ProgressSync<DofusDbTableClientExtensions.MultiSearchQueryProgress> ToMultiSearchProgress(this ProgressSync<ProgressMessage> progress, string message) =>
        new(p =>
            {
                switch (p)
                {
                    case DofusDbTableClientExtensions.MultiSearchCurrentCount currentCount:
                        progress.Report(
                            new ProgressMessage(
                                $"{message} {currentCount.AlreadyFetched}/{currentCount.TotalToFetch}",
                                100.0 * currentCount.AlreadyFetched / currentCount.TotalToFetch,
                                currentCount.AlreadyFetched == currentCount.TotalToFetch
                            )
                        );
                        break;
                }
            }
        );

    public static ProgressSync<DofusDbTableClientExtensions.MultiSearchQueryProgress> ToMultiSearchProgress(
        this ProgressSync<ProgressMessage> progress,
        Func<(int Loaded, int Total), string> messageFactory
    ) =>
        new(p =>
            {
                switch (p)
                {
                    case DofusDbTableClientExtensions.MultiSearchCurrentCount currentCount:
                        progress.Report(
                            new ProgressMessage(
                                messageFactory.Invoke((currentCount.AlreadyFetched, currentCount.TotalToFetch)),
                                100.0 * currentCount.AlreadyFetched / currentCount.TotalToFetch,
                                currentCount.AlreadyFetched == currentCount.TotalToFetch
                            )
                        );
                        break;
                }
            }
        );

    public static void Report(this IProgress<ProgressMessage> progress, string message, double? percent = null, bool done = false) =>
        progress.Report(new ProgressMessage(message, percent, done));

    public static void ReportStep(this IProgress<ProgressMessage> progress, string message, int step, int totalSteps) =>
        progress.Report(message, 100.0 * step / totalSteps, step == totalSteps);
}
