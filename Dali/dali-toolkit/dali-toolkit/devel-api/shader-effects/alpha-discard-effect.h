#ifndef DALI_TOOLKIT_ALPHA_DISCARD_EFFECT_H
#define DALI_TOOLKIT_ALPHA_DISCARD_EFFECT_H

/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd.
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
#include <dali/public-api/object/property-map.h>

// INTERNAL INCLUDES
#include <dali-toolkit/public-api/visuals/visual-properties.h>

namespace Dali
{

namespace Toolkit
{

/**
 * @brief Creates a new Alpha discard effect
 *
 * Alpha discard effect is used to discard fragments when the alpha colour value is below a threshold.
 * This is useful for stenciling.
 *
 * Usage example:
 *
 *   ImageView actor = ImageView::New( EXAMPLE_IMAGE_PATH );
 *   Property::Map alphaDiscardEffect = CreateAlphaDiscardEffect();
 *   actor.SetProperty( ImageView::Property::IMAGE, alphaDiscardEffect );
 *
 * @return A property map of the required shaders.
 */
inline Property::Map CreateAlphaDiscardEffect()
{
  const char* ALPHA_DISCARD_FRAGMENT_SHADER_SOURCE =
      "varying mediump vec2 vTexCoord;                                \n"
      "                                                               \n"
      "uniform sampler2D sTexture;                                    \n"
      "uniform lowp vec4 uColor;                                      \n"
      "void main()                                                    \n"
      "{                                                              \n"
      "  mediump vec4 color = texture2D( sTexture, vTexCoord );       \n"
      "  if(color.a <= 0.0001)                                        \n"
      "  {                                                            \n"
      "    discard;                                                   \n"
      "  }                                                            \n"
      "  gl_FragColor = color * uColor;                               \n"
      "}                                                              \n";

  Property::Map map;

  Property::Map customShader;
  customShader[ Visual::Shader::Property::FRAGMENT_SHADER ] = ALPHA_DISCARD_FRAGMENT_SHADER_SOURCE;

  map[ Toolkit::Visual::Property::SHADER ] = customShader;
  return map;
}

} // namespace Toolkit

} // namespace Dali

#endif // DALI_TOOLKIT_ALPHA_DISCARD_EFFECT_H
