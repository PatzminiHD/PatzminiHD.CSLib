using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.Output
{
    /// <summary>
    /// Class for managing translations of strings in different languages.
    /// </summary>
    /// <typeparam name="Strings">Enum of all strings that should get translated</typeparam>
    /// <typeparam name="Languages">Enum of all languages</typeparam>
    public class Translator<Strings, Languages> where Strings : Enum where Languages : Enum
    {
        private List<LanguageTranslation<Strings, Languages>> _languageTranslations;
        private Languages _defaultLanguage;
        /// <summary>
        /// Create a new Translator instance
        /// </summary>
        /// <param name="languageTranslations">List of translations</param>
        /// <param name="defaultLanguage">Set a default language</param>
        /// <param name="outputWarnings">True to output warnings (about missing translations) to console</param>
        /// <param name="throwIfMissingTranslation">True to throw Exception on missing translations</param>
        public Translator(List<LanguageTranslation<Strings, Languages>> languageTranslations, Languages defaultLanguage, bool outputWarnings = false, bool throwIfMissingTranslation = false)
        {
            _languageTranslations = languageTranslations;
            _defaultLanguage = defaultLanguage;

            CheckAllTranslations(outputWarnings, throwIfMissingTranslation);
        }

        private void CheckAllTranslations(bool outputWarnings, bool throwIfMissingTranslation)
        {
            bool allTranslationsExist = true;
            foreach (Languages language in Enum.GetValues(typeof(Languages)))
            {
                var translations = _languageTranslations.Where(l => EqualityComparer<Languages>.Default.Equals(l.LanguageCode, language));
                if (translations.Count() == 0)
                {
                    allTranslationsExist = false;
                    if (outputWarnings)
                        System.Console.WriteLine($"Warning: No translations found for language '{language}'");
                    continue;
                }
                if (translations.Count() > 1)
                {
                    allTranslationsExist = false;
                    if (outputWarnings)
                        System.Console.WriteLine($"Warning: Multiple translations found for language '{language}'");
                    continue;
                }

                foreach (Strings value in Enum.GetValues(typeof(Strings)))
                {
                    if (!translations.First().Translations.TryGetValue(value, out string? translation) || translation == null)
                    {
                        allTranslationsExist = false;
                        if (outputWarnings)
                        {
                            System.Console.WriteLine($"Warning: Translation for '{value}' in '{translations.First().LanguageCode}' is null");
                        }
                    }
                }
            }
            if (!allTranslationsExist && throwIfMissingTranslation)
            {
                throw new MissingMemberException("Some translations are missing.");
            }
        }

        /// <summary>
        /// Change the default language
        /// </summary>
        /// <param name="newDefaultLanguage"></param>
        public void ChangeDefaultLanguage(Languages newDefaultLanguage)
        {
            _defaultLanguage = newDefaultLanguage;
        }

        /// <summary>
        /// Attempts to retrieve the translation for the specified key and language
        /// </summary>
        /// <remarks>If no translation is found for the specified key and language, the <paramref
        /// name="Value"/> parameter will contain a default value in the format "<c>Strings.Language.Key</c>".</remarks>
        /// <param name="key">The key representing the string to be translated</param>
        /// <param name="language">The language for which the translation is requested</param>
        /// <param name="Value">When this method returns, contains the translation associated with the specified key and language, if the
        /// translation exists; otherwise, contains a default value indicating the key and language. This parameter is
        /// passed uninitialized</param>
        /// <returns><see langword="true"/> if a translation for the specified key and language is found; otherwise, <see
        /// langword="false"/></returns>
        public bool TryGetValue(Strings key, Languages language, out string Value)
        {
            foreach (var languageTranslation in _languageTranslations)
            {
                if (EqualityComparer<Languages>.Default.Equals(languageTranslation.LanguageCode, language))
                {
                    if (languageTranslation.Translations.TryGetValue(key, out string? translation))
                    {
                        Value = translation;
                        return true;
                    }
                }
            }
            Value = $"{nameof(Strings)}.{language.ToString()}.{key.ToString()}";
            return false;
        }

        /// <summary>
        /// Retrieves the localized string for the specified key and language
        /// </summary>
        /// <param name="key">The key identifying the string to retrieve</param>
        /// <param name="language">The language in which the string should be retrieved</param>
        /// <returns>The localized string corresponding to the specified key and language.  Returns a default string if the key or
        /// language does not exist.</returns>
        public string Get(Strings key, Languages language)
        {
            TryGetValue(key, language, out string value);
            return value;
        }

        /// <summary>
        /// Retrieves the string value associated with the specified key for the default language
        /// </summary>
        /// <param name="key">The key used to look up the string value</param>
        /// <returns>The string value associated with the specified key for the default language. If the key is not found,
        /// returns a default string.</returns>
        public string Get(Strings key)
        {
            TryGetValue(key, _defaultLanguage, out string value);
            return value;
        }
    }

    /// <summary>
    /// Represents a collection of translations for a specific language
    /// </summary>
    /// <remarks>This class associates a language code with a dictionary of translations, where each key
    /// corresponds to a specific string identifier and the value is the translated text</remarks>
    /// <typeparam name="Strings">An enumeration type representing the keys for the translatable strings.</typeparam>
    /// <typeparam name="Languages">An enumeration type representing the supported languages.</typeparam>
    public class LanguageTranslation<Strings, Languages> where Strings : Enum where Languages : Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageTranslation{Strings, Languages}"/> class with the specified language code
        /// and translations
        /// </summary>
        /// <param name="languageCode">The language code representing the target language for the translations.</param>
        /// <param name="translations">A dictionary containing the translations, where the keys are <typeparamref name="Strings"/> identifiers and the
        /// values are the corresponding translated strings.</param>
        public LanguageTranslation(Languages languageCode, Dictionary<Strings, string> translations)
        {
            LanguageCode = languageCode;
            Translations = translations;
        }
        /// <summary>
        /// Represents a collection of translations where the key is a localized string identifier and the value is the
        /// corresponding translated text
        /// </summary>
        public Dictionary<Strings, string> Translations;
        /// <summary>
        /// Represents the language code associated with a specific language
        /// </summary>
        /// <remarks>This field is used to identify a language using a predefined enumeration of language
        /// codes</remarks>
        public Languages LanguageCode;
    }
}
