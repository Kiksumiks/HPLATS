#pragma once

//#include <thread>
//#include <atomic>
#include <vector>

extern "C" {
#include "../Parser/Parser.h"
}

struct LogMetaData {
	LogEntry* entryPtr;
	uint64_t timestamp;
	uint8_t level;
	uint8_t thread_id;
	uint8_t padding[6];
};


class LogEngine {
private:
	// From parser
	char* m_fileBuffer;
	LogEntry* m_entries;
	int m_entryCount;

	// Metadata
	std::vector<LogEntry*> m_requestedEntries;
	std::vector<LogMetaData> m_metadata;

	int m_infoCount;
	int m_debugCount;
	int m_warningCount;
	int m_errorCount;
	int m_fatalCount;

	void BuildMetadata();

public:

	LogEngine() :
		m_fileBuffer(nullptr),
		m_entries(nullptr),
		m_entryCount(0),
		m_infoCount(0),
		m_debugCount(0),
		m_warningCount(0),
		m_errorCount(0),
		m_fatalCount(0) {}

	~LogEngine() {
		free_log_entrys(m_entries, m_fileBuffer);
	}


	bool LoadFile(const char* filepath);	
	int ProcessRequestAndGetCount(uint8_t fLevel, uint8_t fThread, uint64_t fMinTime, uint64_t fMaxTime);

	const std::vector<LogEntry*>& GetRequestedEntries() { return m_requestedEntries; }
	int GetTotalEntryCount() const { return m_entryCount; }
	int GetInfoCount() const { return m_infoCount; }
	int GetDebugCount() const { return m_debugCount; }
	int GetWarningCount() const { return m_warningCount; }
	int GetErrorCount() const { return m_errorCount; }
	int GetFatalCount() const { return m_fatalCount; }

};