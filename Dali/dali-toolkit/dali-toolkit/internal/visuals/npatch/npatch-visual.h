#ifndef DALI_TOOLKIT_INTERNAL_N_PATCH_VISUAL_H
#define DALI_TOOLKIT_INTERNAL_N_PATCH_VISUAL_H

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
#include <dali/public-api/common/intrusive-ptr.h>
#include <dali/public-api/images/image.h>
#include <dali/public-api/images/image-operations.h>
#include <dali/devel-api/images/nine-patch-image.h>
#include <dali/public-api/rendering/geometry.h>
#include <dali/public-api/rendering/sampler.h>
#include <dali/public-api/rendering/shader.h>

// INTERNAL INCLUDES
#include <dali-toolkit/internal/visuals/visual-base-impl.h>
#include <dali-toolkit/internal/visuals/visual-url.h>

namespace Dali
{

namespace Toolkit
{

namespace Internal
{

class NPatchVisual;
typedef IntrusivePtr< NPatchVisual > NPatchVisualPtr;

/**
 * The visual which renders an 9 patch image to the control's quad
 *
 * The following properties are optional
 *
 * | %Property Name           | Type             |
 * |--------------------------|------------------|
 * | url                      | STRING           |
 * | borderOnly               | BOOLEAN          |
 * | border                   | RECTANGLE        |
 * | auxiliaryImage           | STRING           |
 * | auxiliaryImageAlpha      | FLOAT            |
 */
class NPatchVisual: public Visual::Base
{
public:

  /**
   * @brief Create an N-patch visual using an image URL.
   *
   * The visual will load the image synchronously when the associated actor is put on stage, and destroy the image when it is off stage
   *
   * @param[in] factoryCache A pointer pointing to the VisualFactoryCache object
   * @param[in] imageUrl The URL to 9 patch image resource to use
   * @param[in] properties A Property::Map containing settings for this visual
   * @return A smart-pointer to the newly allocated visual.
   */
  static NPatchVisualPtr New( VisualFactoryCache& factoryCache, const VisualUrl& imageUrl, const Property::Map& properties );

  /**
   * @brief Create an N-patch visual using an image URL.
   *
   * The visual will load the image synchronously when the associated actor is put on stage, and destroy the image when it is off stage
   *
   * @param[in] factoryCache A pointer pointing to the VisualFactoryCache object
   * @param[in] imageUrl The URL to 9 patch image resource to use
   * @return A smart-pointer to the newly allocated visual.
   */
  static NPatchVisualPtr New( VisualFactoryCache& factoryCache, const VisualUrl& imageUrl );

  /**
   * @brief Create an N-patch visual with a NinePatchImage resource.
   *
   * @param[in] factoryCache A pointer pointing to the VisualFactoryCache object
   * @param[in] image The NinePatchImage to use
   */
  static NPatchVisualPtr New( VisualFactoryCache& factoryCache, NinePatchImage image );

public:  // from Visual

  /**
   * @copydoc Visual::Base::GetNaturalSize
   */
  virtual void GetNaturalSize( Vector2& naturalSize );

  /**
   * @copydoc Visual::Base::CreatePropertyMap
   */
  virtual void DoCreatePropertyMap( Property::Map& map ) const;

  /**
   * @copydoc Visual::Base::CreateInstancePropertyMap
   */
  virtual void DoCreateInstancePropertyMap( Property::Map& map ) const;

protected:

  /**
   * @brief Constructor.
   *
   * @param[in] factoryCache Reference to the VisualFactoryCache object
   */
  NPatchVisual( VisualFactoryCache& factoryCache );

  /**
   * @brief A reference counted object may only be deleted by calling Unreference().
   */
  virtual ~NPatchVisual();

  /**
   * @copydoc Visual::Base::DoSetProperties
   */
  virtual void DoSetProperties( const Property::Map& propertyMap );

  /**
   * @copydoc Visual::Base::DoSetOnStage
   */
  virtual void DoSetOnStage( Actor& actor );

  /**
   * @copydoc Visual::Base::DoSetOffStage
   */
  virtual void DoSetOffStage( Actor& actor );

  /**
   * @copydoc Visual::Base::OnSetTransform
   */
  virtual void OnSetTransform();

private:

  /**
   * Loads the NPatch image and the Auxiliary image if needed
   */
  void LoadImages();

  /**
   * @brief Creates a geometry for this renderer's grid size
   *
   * @return Returns the created geometry for this renderer's grid size
   */
  Geometry CreateGeometry();

  /**
   * @brief Creates a shader for this renderer's grid size
   *
   * @return Returns the created shader for this renderer's grid size
   */
  Shader CreateShader();

  /**
   * @brief Applies texture and related uniforms
   */
  void ApplyTextureAndUniforms();

  /**
   * Helper method to get the default Nine patch geometry from cache or create and store it there
   * @param subType to use
   * @return the geometry
   */
  Geometry GetNinePatchGeometry( VisualFactoryCache::GeometryType subType );

  /**
   * @brief Creates a geometry for the grid size to be used by this visuals' shaders
   *
   * @param[in] gridSize The grid size of the solid geometry to create
   * @return Returns the created geometry for the grid size
   */
  Geometry CreateGridGeometry( Uint16Pair gridSize );

  /**
   * @brief Creates a geometry with the border only for the grid size to be used by this visuals' shaders
   * e.g. a 5x4 grid would create a geometry that would look like:
   *
   *   ---------------------
   *   |  /|  /|  /|  /|  /|
   *   |/  |/  |/  |/  |/  |
   *   ---------------------
   *   |  /|           |  /|
   *   |/  |           |/  |
   *   -----           -----
   *   |  /|           |  /|
   *   |/  |           |/  |
   *   ---------------------
   *   |  /|  /|  /|  /|  /|
   *   |/  |/  |/  |/  |/  |
   *   ---------------------
   *
   * @param[in] gridSize The grid size of the solid geometry to create
   * @return Returns the created geometry for the grid size
   */
  Geometry CreateBorderGeometry( Uint16Pair gridSize );

private:

  NPatchLoader&      mLoader;               ///< reference to N patch loader for fast access
  VisualUrl          mImageUrl;             ///< The url to the N patch to load
  VisualUrl          mAuxiliaryUrl;         ///< An auxiliary image that can be displayed on top of the N-Patch
  std::size_t        mId;                   ///< id of the N patch (from loader/cache)
  Devel::PixelBuffer mAuxiliaryPixelBuffer; ///< pixel buffer of the auxiliary mask image
  bool               mBorderOnly;           ///< if only border is desired
  Rect<int>          mBorder;               ///< The size of the border
  float              mAuxiliaryImageAlpha;  ///< The alpha value for the auxiliary image only
};

} // namespace Internal

} // namespace Toolkit

} // namespace Dali

#endif // DALI_TOOLKIT_INTERNAL_N_PATCH_VISUAL_H
