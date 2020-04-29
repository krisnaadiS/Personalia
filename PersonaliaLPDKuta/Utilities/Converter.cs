using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PersonaliaLPDKuta.Utilities
{
    #region grup converter
    [System.Windows.Markup.ContentProperty("Converters")]
    public class ValueConverterGroup : IValueConverter
    {
        #region Data

        private readonly ObservableCollection<IValueConverter> converters = new ObservableCollection<IValueConverter>();
        private readonly Dictionary<IValueConverter, ValueConversionAttribute> cachedAttributes = new Dictionary<IValueConverter, ValueConversionAttribute>();

        #endregion // Data

        #region Constructor

        public ValueConverterGroup()
        {
            this.converters.CollectionChanged += this.OnConvertersCollectionChanged;
        }

        #endregion // Constructor

        #region Converters

        /// <summary>
        /// Returns the list of IValueConverters contained in this converter.
        /// </summary>
        public ObservableCollection<IValueConverter> Converters
        {
            get { return this.converters; }
        }

        #endregion // Converters

        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object output = value;

            for (int i = 0; i < this.Converters.Count; ++i)
            {
                IValueConverter converter = this.Converters[i];
                Type currentTargetType = this.GetTargetType(i, targetType, true);
                output = converter.Convert(output, currentTargetType, parameter, culture);

                // If the converter returns 'DoNothing' then the binding operation should terminate.
                if (output == Binding.DoNothing)
                    break;
            }

            return output;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object output = value;

            for (int i = this.Converters.Count - 1; i > -1; --i)
            {
                IValueConverter converter = this.Converters[i];
                Type currentTargetType = this.GetTargetType(i, targetType, false);
                output = converter.ConvertBack(output, currentTargetType, parameter, culture);

                // When a converter returns 'DoNothing' the binding operation should terminate.
                if (output == Binding.DoNothing)
                    break;
            }

            return output;
        }

        #endregion // IValueConverter Members

        #region Private Helpers

        #region GetTargetType

        /// <summary>
        /// Returns the target type for a conversion operation.
        /// </summary>
        /// <param name="converterIndex">The index of the current converter about to be executed.</param>
        /// <param name="finalTargetType">The 'targetType' argument passed into the conversion method.</param>
        /// <param name="convert">Pass true if calling from the Convert method, or false if calling from ConvertBack.</param>
        protected virtual Type GetTargetType(int converterIndex, Type finalTargetType, bool convert)
        {
            // If the current converter is not the last/first in the list, 
            // get a reference to the next/previous converter.
            IValueConverter nextConverter = null;
            if (convert)
            {
                if (converterIndex < this.Converters.Count - 1)
                {
                    nextConverter = this.Converters[converterIndex + 1];
                    if (nextConverter == null)
                        throw new InvalidOperationException("The Converters collection of the ValueConverterGroup contains a null reference at index: " + (converterIndex + 1));
                }
            }
            else
            {
                if (converterIndex > 0)
                {
                    nextConverter = this.Converters[converterIndex - 1];
                    if (nextConverter == null)
                        throw new InvalidOperationException("The Converters collection of the ValueConverterGroup contains a null reference at index: " + (converterIndex - 1));
                }
            }

            if (nextConverter != null)
            {
                ValueConversionAttribute conversionAttribute = cachedAttributes[nextConverter];

                // If the Convert method is going to be called, we need to use the SourceType of the next 
                // converter in the list.  If ConvertBack is called, use the TargetType.
                return convert ? conversionAttribute.SourceType : conversionAttribute.TargetType;
            }

            // If the current converter is the last one to be executed return the target type passed into the conversion method.
            return finalTargetType;
        }

        #endregion // GetTargetType

        #region OnConvertersCollectionChanged

        void OnConvertersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // The 'Converters' collection has been modified, so validate that each value converter it now
            // contains is decorated with ValueConversionAttribute and then cache the attribute value.

            IList convertersToProcess = null;
            if (e.Action == NotifyCollectionChangedAction.Add ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                convertersToProcess = e.NewItems;
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (IValueConverter converter in e.OldItems)
                    this.cachedAttributes.Remove(converter);
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                this.cachedAttributes.Clear();
                convertersToProcess = this.converters;
            }

            if (convertersToProcess != null && convertersToProcess.Count > 0)
            {
                foreach (IValueConverter converter in convertersToProcess)
                {
                    object[] attributes = converter.GetType().GetCustomAttributes(typeof(ValueConversionAttribute), false);

                    if (attributes.Length != 1)
                        throw new InvalidOperationException("All value converters added to a ValueConverterGroup must be decorated with the ValueConversionAttribute attribute exactly once.");

                    this.cachedAttributes.Add(converter, attributes[0] as ValueConversionAttribute);
                }
            }
        }

        #endregion // OnConvertersCollectionChanged

        #endregion // Private Helpers
    }
    #endregion

    #region BooleanToVisibilityConverter

    [ValueConversion(typeof(bool), typeof(Visibility))]

    public class BooleanToVisibilityConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value is bool, "value should be a bool");
            Debug.Assert(targetType.IsAssignableFrom(typeof(Visibility)), "targetType should assignable from a Visibility");
            if (((bool)value) == true)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value is Visibility, "value should be a visibility");
            Debug.Assert(targetType.IsAssignableFrom(typeof(bool)), "targetType should assignable from a bool");
            if ((value as Visibility?).Value.Equals(Visibility.Visible))
                return true;
            else
                return false;
        }
    }
    #endregion

    #region InvertedBooleanConverter

    [ValueConversion(typeof(bool), typeof(bool))]

    public class InvertedBooleanConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value is bool, "value should be a bool");
            Debug.Assert(targetType.IsAssignableFrom(typeof(bool)), "targetType should assignable from a bool");
            if ((value as bool?).Value == true)
                return false;
            else
                return true;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value is bool, "value should be a bool");
            Debug.Assert(targetType.IsAssignableFrom(typeof(bool)), "targetType should assignable from a bool");
            if ((value as bool?).Value == true)
                return false;
            else
                return true;
        }
    }
    #endregion

    #region IntegerToBooleanConverter

    [ValueConversion(typeof(int), typeof(bool))]

    public class IntegerToBooleanConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value is int, "value should be a int");
            Debug.Assert(targetType.IsAssignableFrom(typeof(bool)), "targetType should assignable from a bool");
            if ((int)value == 1)
                return true;
            else
                return false;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value is bool, "value should be a bool");
            Debug.Assert(targetType.IsAssignableFrom(typeof(int)), "targetType should assignable from a int");
            if ((value as bool?).Value == true)
                return 1;
            else
                return 0;
        }
    }
    #endregion
}
