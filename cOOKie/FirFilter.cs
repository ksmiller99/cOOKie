/*
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
using System.IO;
using System.Windows.Forms;

using System.Numerics;
using Newtonsoft.Json;

namespace cOOKie
{
    
    ///**
    // * Deallocated and deinitialize resources used by the provided FIR filter
    // *
    // * @param   filt    Filter handle
    // */
    //void fir_deinit(struct fir_filter *filter);

    ///**
    // * Reset and clear the history of the provided filter
    // *
    // * @param   filt    Filter handle
    // */
    //void fir_reset(struct fir_filter *filter);

    ///**
    // * Get the total decimation of all filter stages
    // *
    // * @param   filt    Filter handle
    // *
    // * @return Total decimation, or 0 on error
    // */
    //unsigned int fir_get_total_decimation(struct fir_filter *filter);

    ///**
    // * Perform filtering and decmation operation
    // *
    // * @param[in]   filter  Filter handle
    // * @param[in]   input   Complex input signal
    // * @param[in]   count   Number of samples in the input signal
    // * @param[out]  output  Filtered output. Buffer must be large enough to
    // *                      fit (count / total_decimation) samples.
    // *
    // * @return Number of items written to output
    // */
    //size_t fir_filter_and_decimate(struct fir_filter *filter,
    //                               const struct complexf *input, size_t count,
    //                               struct complexf *output);

    
    class FirFilter 
    {
        public Filter filter;
        int maxInput;
        uint totalDecimation;

        public static FirFilter init(string jsonFile, int max_input)
        {
            int status = -1;
            int i;

            uint totalDecimation = 1;
            FirFilter ff = null;
            try
            {
                ff = JsonConvert.DeserializeObject<FirFilter>(File.ReadAllText(jsonFile));
                if (ff.filter == null || ff.filter.stages == null || (ff.filter.stages.Length == 0))
                {
                    throw new Exception("Malformed Filter");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"Error");
                ff = null;
            }
            return ff;

        }

        
    }

    
    class FirStage {
        public uint decimation;
        public double[] taps;
    }

    class Filter {
        public string comment;
        public FirStage[] stages;
    }
    
    //struct FF {
    //    public Filter filter;
    //}

    /**
         * Load and initialize an FIR filter from the specified JSON file
         *
         * @param   json        JSON file containing description of FIR filter
         *
         * @param   max_input   Maximum number of samples that will be provided
         *                      to fir_filter_and_decimate(). This determines the
         *                      size of internal buffers.
         *
         * @return  Pointer to fir_filter handle on success, or NULL on failure.
         *          The caller is responsible for calling fir_deinit() to deallocate
         *          and deinitialize the returned filter handle.
         */
        

}
