//
// Copyright (c) 2017 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

#ifndef HAL_NF_COMMUNITY_H
#define HAL_NF_COMMUNITY_H


// these are for error checks on the configuration header files
#if !defined(HAL_USE_STM32_FLASH)
#define HAL_USE_STM32_FLASH                         FALSE
#endif

// Abstract interfaces

// Shared headers
// #include "hal_nnnn.h"

// Normal drivers
#include "hal_stm32_flash.h"

// Complex drivers
// #include "hal_nnnn.h"

///////////////////////////////////////////////////////////////////////////////
// Driver constants.                                                         //
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Driver pre-compile time settings.                                         //
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Derived constants and error checks.                                       //
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Driver data structures and types.                                         //
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Driver macros.                                                            //
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// External declarations.                                                    //
///////////////////////////////////////////////////////////////////////////////

#ifdef __cplusplus
extern "C" {
#endif

  void halCommunityInit(void);

#ifdef __cplusplus
}
#endif

#endif // HAL_NF_COMMUNITY_H

