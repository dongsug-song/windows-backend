#ifndef DALI_CORE_VERSION_H
#define DALI_CORE_VERSION_H

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
#include <cstdint> // uint32_t

// INTERNAL INCLUDES
#include <dali/public-api/common/dali-common.h>

namespace Dali
{
DALI_CORE_API extern const uint32_t CORE_MAJOR_VERSION; ///< The major version number of the Core library.
DALI_CORE_API extern const uint32_t CORE_MINOR_VERSION; ///< The minor version number of the Core library.
DALI_CORE_API extern const uint32_t CORE_MICRO_VERSION; ///< The micro version number of the Core library.
DALI_CORE_API extern const char * const CORE_BUILD_DATE;    ///< The date/time the Core library was built.
} // namespace Dali

#endif // DALI_CORE_VERSION_H
