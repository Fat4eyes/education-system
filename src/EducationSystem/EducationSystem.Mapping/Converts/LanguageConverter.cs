using System;
using AutoMapper;
using CodeExecutionSystem.Contracts.Data;
using EducationSystem.Enums;

namespace EducationSystem.Mapping.Converts
{
    public sealed class LanguageConverter : IValueConverter<LanguageType, Language>
    {
        public Language Convert(LanguageType source, ResolutionContext context)
        {
            switch (source)
            {
                case LanguageType.PHP:
                    return Language.Php;

                case LanguageType.Pascal:
                    return Language.Pascal;

                case LanguageType.CPP:
                    return Language.CPlusPlus;

                case LanguageType.JavaScript:
                    return Language.Js;

                // Языки ниже не поддерживаются системой выполнения кода.
            
                case LanguageType.C:
                case LanguageType.CP:
                    return Language.Unspecified;
            }

            throw new ArgumentOutOfRangeException(nameof(source));
        }
    }
}