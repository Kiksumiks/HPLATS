#define LOGENGINE_EXPORTS
#include "LogEngineAPI.h"
#include "Engine.h"
#include <cstring>
#include <cstdio>
#include <cstdint>

#pragma pack(push, 1)
typedef struct {
    char date[11];
    char time[9];
    char level[8];
    char thread_id[16];
    char message[256];
} LogEntryManaged;
#pragma pack(pop)


extern "C" {

    LOGENGINE_API void* CreateLogSession(const char* filepath) {
        if (!filepath) return nullptr;
        LogEngine* engine = new LogEngine();
        if (!engine->LoadFile(filepath)) {
            delete engine;
            return nullptr;
        }

        return (void*)engine;
    }

    LOGENGINE_API void CloseLogSession(void* sessionHandle) {
        if (sessionHandle) {
            LogEngine* engine = static_cast<LogEngine*>(sessionHandle);
            delete engine;
        }
    }

    LOGENGINE_API int RequestLogEntries(void* sessionHandle, uint8_t fLevel, uint8_t fThread, uint64_t fMinTime, uint64_t fMaxTime) {
        if (!sessionHandle) return 0;
        LogEngine* engine = static_cast<LogEngine*>(sessionHandle);
        return engine->ProcessRequestAndGetCount(fLevel, fThread, fMinTime, fMaxTime);
    }

    LOGENGINE_API int GetRequestedEntries(void* sessionHandle, void* outBuffer, int maxCount) {
        if (!sessionHandle) return 0;
        LogEngine* engine = static_cast<LogEngine*>(sessionHandle);
        LogEntryManaged* buffer = (LogEntryManaged*)outBuffer;
        const std::vector<LogEntry*> allRequestedEntries = engine->GetRequestedEntries();
        int requestedEntryCount = (int)allRequestedEntries.size();
        int count_to_copy = (requestedEntryCount < maxCount) ? requestedEntryCount : maxCount;
        for (int i = 0; i < count_to_copy; i++) {
            LogEntry* src = allRequestedEntries[i];
            LogEntryManaged* dest = &buffer[i];
            std::memset(dest, 0, sizeof(LogEntryManaged));
            snprintf(dest->date, sizeof(dest->date), "%s", src->date);
            snprintf(dest->time, sizeof(dest->time), "%s", src->time);
            snprintf(dest->level, sizeof(dest->level), "%s", src->level);
            snprintf(dest->thread_id, sizeof(dest->thread_id), "%s", src->thread_id);
            snprintf(dest->message, sizeof(dest->message), "%s", src->message);
        }

        return count_to_copy;
    }

    LOGENGINE_API int GetTotalCount(void* sessionHandle) {
        if (!sessionHandle) return 0;
        LogEngine* engine = static_cast<LogEngine*>(sessionHandle);
        return engine->GetTotalEntryCount();
    }

    LOGENGINE_API void GetLogInfoCounts(void* sessionHandle, int& cInfo, int& cDebug, int& cWarning, int& cError, int& cFatal) {
        if (!sessionHandle) return;
        LogEngine* engine = static_cast<LogEngine*>(sessionHandle);
        
        cInfo = engine->GetInfoCount();
        cDebug = engine->GetDebugCount();
        cWarning = engine->GetWarningCount();
        cError = engine->GetErrorCount();
        cFatal = engine->GetFatalCount();
    }

    LOGENGINE_API bool IsProcessingComplete(void* sessionHandle) {
        if (!sessionHandle) return false;
        LogEngine* engine = static_cast<LogEngine*>(sessionHandle);
        return engine->IsMetadataReady();
    }

}