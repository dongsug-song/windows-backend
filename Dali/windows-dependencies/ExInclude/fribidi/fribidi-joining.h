/* FriBidi
 * fribidi-joining.h - Arabic joining algorithm
 *
 * $Id: fribidi-joining.h,v 1.3 2004/06/21 21:15:31 behdad Exp $
 * $Author: behdad $
 * $Date: 2004/06/21 21:15:31 $
 * $Revision: 1.3 $
 * $Source: /cvs/fribidi/fribidi2/lib/fribidi-joining.h,v $
 *
 * Authors:
 *   Behdad Esfahbod, 2004
 *
 * Copyright (C) 2004 Sharif FarsiWeb, Inc
 * Copyright (C) 2004 Behdad Esfahbod
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public License
 * along with this library, in a file named COPYING; if not, write to the
 * Free Software Foundation, Inc., 59 Temple Place, Suite 330,
 * Boston, MA 02111-1307, USA
 * 
 * For licensing issues, contact <license@farsiweb.info>.
 */
#ifndef _FRIBIDI_JOINING_H
#define _FRIBIDI_JOINING_H

#include "fribidi-common.h"

#include "fribidi-types.h"
#include "fribidi-bidi-types.h"
#include "fribidi-joining-types.h"

#include "fribidi-begindecls.h"

#define fribidi_join_arabic FRIBIDI_NAMESPACE(join_arabic)
/* fribidi_join_arabic - do Arabic joining
 *
 * This function does the Arabic joining algorithm.  Means, given Arabic
 * joining types of the characters in ar_props (don't worry,
 * FriBidiJoiningType can be casted to FriBidiArabicProp automagically), this
 * function modifies this properties to grasp the effect of neighboring
 * characters.  You probably need this information later to do Arabic shaping.
 *
 * This function implements rules R1 to R7 inclusive (all rules) of the Arabic
 * Cursive Joining algorithm of the Unicode standard as available at 
 * http://www.unicode.org/versions/Unicode4.0.0/ch08.pdf#G7462.  It also
 * interacts correctly with the bidirection algorithm as defined in Section
 * 3.5 Shaping of the Unicode Bidirectional Algorithm available at
 * http://www.unicode.org/reports/tr9/#Shaping.
 *
 * There are a few macros defined in fribidi-joining-types.h for querying the
 * Arabic properties computed by this function.
 */
FRIBIDI_ENTRY void fribidi_join_arabic (
  const FriBidiCharType *bidi_types,	/* input list of bidi types as
					   returned by
					   fribidi_get_bidi_types() */
  const FriBidiStrIndex len,	/* input string length */
  const FriBidiLevel *embedding_levels,	/* input list of embedding
					   levels, as returned by
					   fribidi_get_par_embedding_levels */
  FriBidiArabicProp *ar_props	/* Arabic properties to analyze, initilized by
				   joining types, as returned by
				   fribidi_get_joining_types */
);

#include "fribidi-enddecls.h"

#endif /* !_FRIBIDI_JOINING_H */
/* Editor directions:
 * vim:textwidth=78:tabstop=8:shiftwidth=2:autoindent:cindent
 */
