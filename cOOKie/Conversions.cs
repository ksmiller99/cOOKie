/*
 * This file is a modified version of the conversions.c included in
 * the bladeRF project: http://www.github.com/nuand/bladeRF
 *
 * Copyright (c) 2013 Nuand LLC
 * Copyright (c) 2015 Jon Szymaniak <jon.szymaniak@gmail.com>
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cOOKie
{
    class Conversions
    {
        /**
         * Represents an association between a string suffix for a numeric value and
         * its multiplier. For example, "k" might correspond to 1000.
         */
        public struct Numeric_suffix
        {
            private readonly String suffix;
            private readonly uint multiplier;

            public Numeric_suffix(String suffix, uint multiplier)
            {
                this.suffix = suffix;
                this.multiplier = multiplier;
            }

            public String Suffix { get { return suffix; } }
            public uint Multiplier { get { return multiplier; } }

        }

        public static Conversions.Numeric_suffix[] hz_suffixes =
            new Conversions.Numeric_suffix[] {
            new Conversions.Numeric_suffix("K",   1000),
            new Conversions.Numeric_suffix("KHz", 1000),
            new Conversions.Numeric_suffix("M",   1000000),
            new Conversions.Numeric_suffix("MHz", 1000000),
            new Conversions.Numeric_suffix("G",   1000000000),
            new Conversions.Numeric_suffix("GHz", 1000000000),
        };

        /**
         * String to integer conversion with range and error checking
         *
         *  @param  str     String to convert
         *  @param  min     Inclusive minimum allowed value
         *  @param  max     Inclusive maximum allowed value
         *  @param  ok      If non-NULL, this will be set to true to denote that
         *                  the conversion succeeded. If this value is not true,
         *                  then the return value should not be used.
         *
         *  @return 0 on success, undefined on failure (ask Jon?)
         */

        public static int str2int(String str, int min, int max, out bool ok)
        {
            int value;

            if (Int32.TryParse(str, out value) && (value >= min) && (value <= max))
            {
                ok = true;
                return value;
            }

            foreach (Numeric_suffix sf in hz_suffixes)
            {
                if (str.EndsWith(sf.Suffix))
                {
                    value *= (int)sf.Multiplier;
                    break;
                }
            }

            if (value >= min && value <= max)
            {
                ok = true;
                return value;
            }
            else
            {
                ok = false;
                return 0;
            }
        }



        /**
         * Convert a string to an unsigned inteager with range and error checking
         *
         * @param[in]   str     String to conver
         * @param[in]   min     Minimum allowed value (inclusive)
         * @param[in]   max     Maximum allowed value (inclusive)
         * @param[out]  ok      If non-NULL, this is set to true if the conversion was
         *                      successful, and false for an invalid or out of range
         *                      value.
         *
         * @return  Converted value on success, 0 on failure
         */
        public static uint str2uint(String str, uint min, uint max, out bool ok)
        {

            uint value;

            if (!UInt32.TryParse(str, out value) || value < min || value > max)
            {
                ok = false;
                return 0;
            }

            ok = true;
            return value;

        }

        /**
         * Convert a string to a signed 64-bit integer with range and error checking
         *
         * @param[in]   str     String to conver
         * @param[in]   min     Minimum allowed value (inclusive)
         * @param[in]   max     Maximum allowed value (inclusive)
         * @param[out]  ok      If non-NULL, this is set to true if the conversion was
         *                      successful, and false for an invalid or out of range
         *                      value.
         *
         * @return  Converted value on success, 0 on failure
         */
        public Int64 str2int64(String str, Int64 min, Int64 max, ref bool ok)
        {

            Int64 value;

            if (!Int64.TryParse(str, out value) || value < min || value > max)
            {
                ok = false;
                return 0;
            }

            ok = true;
            return value;

        }

        /**
         * Convert a string to an unsigned 64-bit integer with range and error checking
         *
         * @param[in]   str     String to conver
         * @param[in]   min     Minimum allowed value (inclusive)
         * @param[in]   max     Maximum allowed value (inclusive)
         * @param[out]  ok      If non-NULL, this is set to true if the conversion was
         *                      successful, and false for an invalid or out of range
         *                      value.
         *
         * @return  Converted value on success, 0 on failure
         */
        UInt64 str2uint64(String str, UInt64 min, UInt64 max, ref bool ok)
        {

            UInt64 value;

            if (!UInt64.TryParse(str, out value) || value < min || value > max)
            {
                ok = false;
                return 0;
            }

            ok = true;
            return value;

        }

        /**
         * Convert a string to a double with range and error checking
         *
         * @param[in]   str     String to conver
         * @param[in]   min     Minimum allowed value (inclusive)
         * @param[in]   max     Maximum allowed value (inclusive)
         * @param[out]  ok      If non-NULL, this is set to true if the conversion was
         *                      successful, and false for an invalid or out of range
         *                      value.
         *
         * @return  Converted value on success, 0 on failure
         */
        public static double str2double(String str, double min, double max, ref bool ok)
        {

            double value;

            if (!Double.TryParse(str, out value) || value < min || value > max)
            {
                ok = false;
                return 0;
            }

            ok = true;
            return value;

        }

        /**
         * Convert a string to an unsigned integer with range and error checking.
         * Supports the use of decimal representations and suffixes in the string.
         * For example, a string "2.4G" might be converted to 2400000000.
         *
         * @param[in]   str             String to convert
         * @param[in]   min             Minimum allowed value (inclusive)
         * @param[in]   max             Maximum allowed value (inclusive)
         * @param[in]   suffixes        List of allowed suffixes and their multipliers
         * @param[out]  ok              If non-NULL, this is set to true if the conversion was
         *                              successful, and false for an invalid or out of range
         *                              value.
         *
         * @return  Converted value on success, 0 on failure
         */
        public static uint str2uint_suffix(String str,
                             uint min,
                             uint max,
                             Numeric_suffix[] suffixes, 
                             out bool ok)
        {
            double value;
            uint multiplier = 0; //use zero to flag suffix not found

           
            /* check if no suffix */
            if (Double.TryParse(str, out value))
            {
                multiplier = 1;
            }
            else
            {
                /* Loop through each available suffix */
                foreach (Numeric_suffix sf in suffixes) 
                {
                    /* If the suffix appears at the end of the number */
                    if (str.EndsWith(sf.Suffix)) 
                    {
                        /* Extract the value */
                        if(!Double.TryParse(str.Substring(0,str.Length-sf.Suffix.Length), out value))
                        {
                           ok = false;
                           return 0;
                        }
                        multiplier = sf.Multiplier;
                        break;
                    }
                }
            }

            /* check if suffix not found */
            if (multiplier == 0)
            {
                ok = false;
                return 0;
            }

            value *= multiplier;

             /* Check that the resulting value is in bounds */
             if (value > max || value < min) {
                ok = false;
                return 0;
             }

             ok = true;

            /* Truncate the floating point value to an integer and return it */
            return (uint) value;
        }

    //    /**
    //     * Convert a string to a log level enumeration value
    //     *
    //     * @parma[in]   str   Input string
    //     * @param[out]  ok    The string was a valid value
    //     *
    //     * @return log level value if *ok == true, undefined otherwise
    //     */
    //    public static log_level str2loglevel(String str, out bool ok)
    //    {
    //        log_level level = log_level.LOG_LEVEL_ERROR;
    //        bool valid = true;

    //        if (str.ToLower().Equals("critical")) 
    //        {
    //            level = log_level.LOG_LEVEL_CRITICAL;
    //        }
    //        else if (str.ToLower().Equals("error"))
    //        {
    //            level = log_level.LOG_LEVEL_ERROR;
    //        } 
    //        else if (str.ToLower().Equals("warning")) 
    //        {
    //            level = log_level.LOG_LEVEL_WARNING;
    //        } 
    //        else if (str.ToLower().Equals("info")) 
    //        {
    //            level = log_level.LOG_LEVEL_INFO;
    //        } 
    //        else if (str.ToLower().Equals("debug")) 
    //        {
    //            level = log_level.LOG_LEVEL_DEBUG; 
    //        }
    //        else if (str.ToLower().Equals("verbose"))
    //        {
    //            level = log_level.LOG_LEVEL_VERBOSE;
    //        }
    //        else if (str.ToLower().Equals("silent"))
    //        {
    //            level = log_level.LOG_LEVEL_SILENT;
    //        }
    //        else 
    //        {
    //            valid = false;
    //        }

    //    ok = valid;
    //    return level;

    //    }
    }

}
