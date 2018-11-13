#ifndef __DALI_TOOLKIT_TEXT_MARKUP_PROCESSOR_H__
#define __DALI_TOOLKIT_TEXT_MARKUP_PROCESSOR_H__

/*
 * Copyright (c) 2015 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

// EXTERNAL INCLUDES
#include <dali/public-api/common/dali-vector.h>
#include <string>

// INTERNAL INCLUDES
#include <dali-toolkit/internal/text/color-run.h>
#include <dali-toolkit/internal/text/font-description-run.h>

namespace Dali
{

namespace Toolkit
{

namespace Text
{

/**
 * @brief Keeps the plain text and references to vectors from the model which stores runs with text styles.
 */
struct MarkupProcessData
{
MarkupProcessData( Vector<ColorRun>& colorRuns,
                   Vector<FontDescriptionRun>& fontRuns )
  : colorRuns( colorRuns ),
    fontRuns( fontRuns ),
    markupProcessedText()
  {}

  Vector<ColorRun>&           colorRuns;           ///< The color runs.
  Vector<FontDescriptionRun>& fontRuns;            ///< The font description runs.

  std::string                 markupProcessedText; ///< The mark-up string.
};

/**
 * @brief Process the mark-up string.
 *
 * @param[in] markupString The mark-up string.
 * @param[out] markupProcessData The plain text and the style.
 */
void ProcessMarkupString( const std::string& markupString, MarkupProcessData& markupProcessData );

} // namespace Text

} // namespace Toolkit

} // namespace Dali

#endif // __DALI_TOOLKIT_TEXT_MARKUP_PROCESSOR_H__
