#pragma once


#ifdef LOGENGINE_EXPORTS
#define LOGENGINE_API __declspec(dllexport)
#else
#define LOGENGINE_API __declspec(dllimport)
#endif


/*
#pragma pack(push, 1)
typedef struct {
	char date[11];
	char time[9];
	char level[8];
	char thread_id[16];
	char message[256];
} LogEntryManaged;
#pragma pack(pop)
*/


extern "C" {

    LOGENGINE_API void* CreateLogSession(const char* filepath);

    LOGENGINE_API int GetSessionCount(void* sessionHandle);

	LOGENGINE_API void CloseLogSession(void* sessionHandle);

    LOGENGINE_API int GetLogEntries(void* sessionHandle, void* outBuffer, int maxCount);

    //LOGENGINE_API int GetLogLevelCount(const char* levelName);
}