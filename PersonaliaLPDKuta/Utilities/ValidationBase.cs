using PersonaliaLPDKuta.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PersonaliaLPDKuta.Utilities
{
	public class ValidationBase : BaseModel, INotifyDataErrorInfo
	{
        public bool HasErrors
        {
            get
            {
                return GetErrors(null).OfType<object>().Any();
            }
        }

        public virtual void ForceValidation()
        {
            OnPropertyChanged(null);
        }

        public virtual IEnumerable GetErrors([CallerMemberName] string propertyName = null)
        {
            return Enumerable.Empty<object>();
        }

        protected void OnErrorsChanged([CallerMemberName] string propertyName = null)
        {
            OnErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected virtual void OnErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            var handler = ErrorsChanged;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}
