#ifndef DALI_TOOLKIT_INTERNAL_WIREFRAME_VISUAL_H
#define DALI_TOOLKIT_INTERNAL_WIREFRAME_VISUAL_H

/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd.
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

// INTERNAL INCLUDES
#include <dali-toolkit/internal/visuals/visual-base-impl.h>

namespace Dali
{

namespace Toolkit
{

namespace Internal
{

class WireframeVisual;
typedef IntrusivePtr< WireframeVisual > WireframeVisualPtr;

/**
 * @brief Renders a wireframe outline to the control's quad.
 */
class WireframeVisual: public Visual::Base
{
public:

  /**
   * @brief Create a new wireframe visual.
   *
   * @param[in] factoryCache A pointer pointing to the VisualFactoryCache object
   * @param[in] properties A Property::Map containing settings for this visual
   * @return A smart-pointer to the newly allocated visual.
   */
  static WireframeVisualPtr New( VisualFactoryCache& factoryCache, const Property::Map& properties );

  /**
   * @brief Create a new wireframe visual with an encapsulated actual visual.
   *
   * For debugging purpose, the rendering of the encapsulated visual is replaced with wireframe
   *
   * @param[in] factoryCache A pointer pointing to the VisualFactoryCache object
   * @param[in] actualVisual The encapsulated actual visual.
   * @return A smart-pointer to the newly allocated visual.
   */
  static WireframeVisualPtr New( VisualFactoryCache& factoryCache, Visual::BasePtr actualVisual );

  /**
   * @brief Create a new wireframe visual with an encapsulated actual visual.
   *
   * For debugging purpose, the rendering of the encapsulated visual is replaced with wireframe
   *
   * @param[in] factoryCache A pointer pointing to the VisualFactoryCache object
   * @param[in] actualVisual The encapsulated actual visual.
   * @param[in] properties A Property::Map containing settings for this visual
   * @return A smart-pointer to the newly allocated visual.
   */
  static WireframeVisualPtr New( VisualFactoryCache& factoryCache, Visual::BasePtr actualVisual, const Property::Map& properties );


protected:

  /**
   * @brief Constructor.
   *
   * @param[in] factoryCache A pointer pointing to the VisualFactoryCache object
   * @param[in] actualVisual The encapsulated actual visual.
   */
  WireframeVisual( VisualFactoryCache& factoryCache, Visual::BasePtr actualVisual );

  /**
   * @brief A reference counted object may only be deleted by calling Unreference().
   */
  virtual ~WireframeVisual();

protected: // from Visual::Base

  /**
   * @copydoc Visual::Base::GetHeightForWidth()
   */
  virtual float GetHeightForWidth( float width );

  /**
   * @copydoc Visual::Base::GetNaturalSize()
   */
  virtual void GetNaturalSize( Vector2& naturalSize );

  /**
   * @copydoc Visual::Base::CreatePropertyMap()
   */
  virtual void DoCreatePropertyMap( Property::Map& map ) const;

  /**
   * @copydoc Visual::Base::CreateInstancePropertyMap
   */
  virtual void DoCreateInstancePropertyMap( Property::Map& map ) const;

  /**
   * @copydoc Visual::Base::DoSetProperties()
   */
  virtual void DoSetProperties( const Property::Map& propertyMap );

  /**
   * @copydoc Visual::Base::DoSetOnStage
   */
  virtual void DoSetOnStage( Actor& actor );

  /**
   * @copydoc Visual::Base::OnSetTransform
   */
  virtual void OnSetTransform();

  /**
   * @copydoc Visual::Base::GetVisualObject
   *
   * Overriding as this visual can sometimes act as a proxy to the actual visual, i.e. when using debug rendering.
   */
  virtual Base& GetVisualObject() override;

private:
  /**
   * Create the geometry which presents the quad wireframe.
   * @return The border geometry
   */
  Geometry CreateQuadWireframeGeometry();

  /**
   * @brief Initialise the renderer from the cache, if not available, create and save to the cache for sharing.
   */
  void InitializeRenderer();

  // Undefined
  WireframeVisual( const WireframeVisual& visual);

  // Undefined
  WireframeVisual& operator=( const WireframeVisual& visual );

private:

  Visual::BasePtr mActualVisual;

};

} // namespace Internal

} // namespace Toolkit

} // namespace Dali

#endif // DALI_TOOLKIT_INTERNAL_WIREFRAME_VISUAL_H
