using Pl0.Core;

namespace Pl0.Ide;

internal static class IdeCompilerOptionsRules
{
    internal const int MinMaxLevel = 0;
    internal const int MaxMaxLevel = 10;
    internal const int MinMaxAddress = 127;
    internal const int MaxMaxAddress = 32767;
    internal const int MinMaxIdentifierLength = 1;
    internal const int MaxMaxIdentifierLength = 32;
    internal const int MinMaxSymbolCount = 10;
    internal const int MaxMaxSymbolCount = 5000;
    internal const int MinMaxCodeLength = 50;
    internal const int MaxMaxCodeLength = 10000;
    internal const int MaxNumberDigitsClassic = 14;
    internal const int MaxNumberDigitsExtended = 10;

    internal static CompilerOptions GetResetDefaults()
    {
        return Normalize(CompilerOptions.Default);
    }

    internal static int GetDerivedMaxNumberDigits(Pl0Dialect dialect)
    {
        return dialect == Pl0Dialect.Classic ? MaxNumberDigitsClassic : MaxNumberDigitsExtended;
    }

    internal static CompilerOptions Normalize(CompilerOptions options)
    {
        return options with
        {
            MaxNumberDigits = GetDerivedMaxNumberDigits(options.Dialect)
        };
    }

    internal static bool TryValidateAndNormalize(CompilerOptions candidate, out CompilerOptions normalized, out string? validationError)
    {
        if (!IsInRange(candidate.MaxLevel, MinMaxLevel, MaxMaxLevel))
        {
            normalized = default!;
            validationError = $"MaxLevel muss zwischen {MinMaxLevel} und {MaxMaxLevel} liegen.";
            return false;
        }

        if (!IsInRange(candidate.MaxAddress, MinMaxAddress, MaxMaxAddress))
        {
            normalized = default!;
            validationError = $"MaxAddress muss zwischen {MinMaxAddress} und {MaxMaxAddress} liegen.";
            return false;
        }

        if (!IsInRange(candidate.MaxIdentifierLength, MinMaxIdentifierLength, MaxMaxIdentifierLength))
        {
            normalized = default!;
            validationError = $"MaxIdentifierLength muss zwischen {MinMaxIdentifierLength} und {MaxMaxIdentifierLength} liegen.";
            return false;
        }

        if (!IsInRange(candidate.MaxSymbolCount, MinMaxSymbolCount, MaxMaxSymbolCount))
        {
            normalized = default!;
            validationError = $"MaxSymbolCount muss zwischen {MinMaxSymbolCount} und {MaxMaxSymbolCount} liegen.";
            return false;
        }

        if (!IsInRange(candidate.MaxCodeLength, MinMaxCodeLength, MaxMaxCodeLength))
        {
            normalized = default!;
            validationError = $"MaxCodeLength muss zwischen {MinMaxCodeLength} und {MaxMaxCodeLength} liegen.";
            return false;
        }

        normalized = Normalize(candidate);
        validationError = null;
        return true;
    }

    private static bool IsInRange(int value, int min, int max)
    {
        return value >= min && value <= max;
    }
}
