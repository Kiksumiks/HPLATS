#define LOGENGINE_EXPORTS
#include "LogEngineAPI.h"
#include "Engine.h"
#include <cstring>
#include <cstdio>

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

    LOGENGINE_API int GetSessionCount(void* sessionHandle) {
        if (!sessionHandle) return 0;
        LogEngine* engine = static_cast<LogEngine*>(sessionHandle);
        return engine->GetCount();
    }

    LOGENGINE_API void CloseLogSession(void* sessionHandle) {
        if (sessionHandle) {
            LogEngine* engine = static_cast<LogEngine*>(sessionHandle);
            delete engine;
        }
    }

    LOGENGINE_API int GetLogEntries(void* sessionHandle, void* outBuffer, int maxCount) {      
        if (!sessionHandle || !outBuffer) return 0;

        LogEngine* engine = static_cast<LogEngine*>(sessionHandle);
        LogEntryManaged* buffer = (LogEntryManaged*)outBuffer;
        LogEntry* parsed_buffer = engine->GetEntries();

        int count_to_copy = (engine->GetCount() < maxCount) ? engine->GetCount() : maxCount;

        for(int i = 0; i < count_to_copy; i++) {
            LogEntry* src = &parsed_buffer[i];
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

}