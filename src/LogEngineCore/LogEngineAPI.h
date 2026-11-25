#pragma once

#define LOGENGINE_API __declspec(dllexport)

#include <cstdint>

extern "C" {

    LOGENGINE_API void* CreateLogSession(const char* filepath);

    LOGENGINE_API void CloseLogSession(void* sessionHandle);

    LOGENGINE_API int RequestLogEntries(void* sessionHandle, uint8_t fLevel, uint8_t fThread, uint64_t fMinTime, uint64_t fMaxTime);

    LOGENGINE_API int GetRequestedEntries(void* sessionHandle, void* outBuffer, int maxCount);

    LOGENGINE_API int GetTotalCount(void* sessionHandle);

    LOGENGINE_API void GetLogInfoCounts(void* sessionHandle, int& cInfo, int& cDebug, int& cWarning, int& cError, int& cFatal);

}