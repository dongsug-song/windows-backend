#ifndef DALI_TOOLKIT_TEXT_LAYOUT_PARAMETERS_H
#define DALI_TOOLKIT_TEXT_LAYOUT_PARAMETERS_H

/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
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
#include <dali/public-api/math/vector2.h>

// INTERNAL INCLUDES
#include <dali-toolkit/public-api/text/text-enumerations.h>
#include <dali-toolkit/internal/text/text-definitions.h>
#include <dali-toolkit/internal/text/text-model.h>

namespace Dali
{

namespace Toolkit
{

namespace Text
{

struct BidirectionalLineInfoRun;

namespace Layout
{

/**
 * @brief Struct used to pass parameters.
 */
struct Parameters
{
  /**
   * Constructor with the needed parameters to layout the text.
   *
   * @param[in] boundingBox The size of the box containing the text.
   * @param[in,out] textModel The text's model.
   */
  Parameters( const Vector2& boundingBox,
              ModelPtr textModel )
  : boundingBox{ boundingBox },
    textModel{ textModel },
    lineBidirectionalInfoRunsBuffer{ nullptr },
    numberOfBidirectionalInfoRuns{ 0u },
    startGlyphIndex{ 0u },
    numberOfGlyphs{ 0u },
    startLineIndex{ 0u },
    estimatedNumberOfLines{ 0u },
    interGlyphExtraAdvance{ 0.f },
    isLastNewParagraph{ false }
  {}

  Vector2                         boundingBox;                     ///< The size of the box containing the text.
  ModelPtr textModel;
  BidirectionalLineInfoRun*       lineBidirectionalInfoRunsBuffer; ///< Bidirectional conversion tables per line.
  Length                          numberOfBidirectionalInfoRuns;   ///< The number of lines with bidirectional info.
  GlyphIndex                      startGlyphIndex;                 ///< Index to the first glyph to layout.
  Length                          numberOfGlyphs;                  ///< The number of glyphs to layout.
  LineIndex                       startLineIndex;                  ///< The line index where to insert the new lines.
  Length                          estimatedNumberOfLines;          ///< The estimated number of lines.
  float                           interGlyphExtraAdvance;          ///< Extra advance added to each glyph.
  bool                            isLastNewParagraph:1;            ///< Whether the last character is a new paragraph character.
};

} // namespace Layout

} // namespace Text

} // namespace Toolkit

} // namespace Dali

#endif // DALI_TOOLKIT_TEXT_LAYOUT_PARAMETERS_H
