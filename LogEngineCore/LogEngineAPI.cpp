#define LOGENGINE_EXPORTS
#include "LogEngineAPI.h"
#include "Engine.h"


extern "C" {

    LOGENGINE_API void* CreateLogSession(const char* filepath) {
        LogEngine* engine = new LogEngine();
        if (!engine->LoadFile(filepath)) {
            delete engine;
            return nullptr;
        }

        return (void*)engine;
    }

    LOGENGINE_API int GetSessionCount(void* sessionHandle) {
        if (!sessionHandle) { return 0; }
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
        
        int elementsInBuffer = 0;

        if (sessionHandle) {
            LogEngine* engine = static_cast<LogEngine*>(sessionHandle);
            LogEntry** buffer = (LogEntry**)outBuffer;
            
            for (int i = 0; i < maxCount; i++) {
                buffer[i] = (LogEntry*)engine->GetEntry(i);
                if (buffer[i] == nullptr) break;
                elementsInBuffer++;
            }
        }

        return elementsInBuffer;
    }

}