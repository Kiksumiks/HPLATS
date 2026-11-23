#pragma once

#include <cstdint>

#ifdef LOGENGINE_EXPORTS
#define LOGENGINE_API __declspec(dllexport)
#else
#define LOGENGINE_API __declspec(dllimport)
#endif

extern "C" {

    LOGENGINE_API void* CreateLogSession(const char* filepath);

    LOGENGINE_API void CloseLogSession(void* sessionHandle);

    LOGENGINE_API int RequestLogEntries(void* sessionHandle, uint8_t fLevel, uint8_t fThread, uint64_t fMinTime, uint64_t fMaxTime);

    LOGENGINE_API int GetRequestedEntries(void* sessionHandle, void* outBuffer, int maxCount); // , uint8_t fLevel, uint8_t fThread, uint64_t fMinTime, uint64_t fMaxTime);

    LOGENGINE_API int GetTotalCount(void* sessionHandle);

    LOGENGINE_API void GetLogInfoCounts(void* sessionHandle, int& cInfo, int& cDebug, int& cWarning, int& cError, int& cFatal);

    LOGENGINE_API bool IsProcessingComplete(void* sessionHandle);

}