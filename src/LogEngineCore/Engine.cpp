#include "Engine.h"

static uint64_t ParseTimeStamp(const char* date, const char* time) {

	// YYYY-MM-DD
	uint16_t year = (date[0] - '0') * 1000 + (date[1] - '0') * 100 + (date[2] - '0') * 10 + (date[3] - '0');
	uint8_t month = (date[5] - '0') * 10 + (date[6] - '0');
	uint8_t day = (date[8] - '0') * 10 + (date[9] - '0');

	// HH:MM:SS
	uint8_t hour = (time[0] - '0') * 10 + (time[1] - '0');
	uint8_t min = (time[3] - '0') * 10 + (time[4] - '0');
	uint8_t sec = (time[6] - '0') * 10 + (time[7] - '0');

	uint32_t rDate = ((uint32_t)year << 16) | ((uint32_t)month << 8) | day;
	uint32_t rTime = ((uint32_t)hour << 16) | ((uint32_t)min << 8) | sec;

	uint64_t res = ((uint64_t)rDate << 32) | rTime;

	return res;
}

static uint8_t MapLogLevel(const char* level) {
	// Hmm...
	switch (level[0]) {
	case 'I': return 1; break;
	case 'D': return 2; break;
	case 'W': return 3; break;
	case 'E': return 4; break;
	case 'F': return 5; break;
	default: return 0; break;
	}

}


bool LogEngine::LoadFile(const char* filepath) {

	m_fileBuffer = readFileIntoBuffer(filepath);
	if (!m_fileBuffer) return false;

	m_entries = load_log_file(&m_entryCount, m_fileBuffer); // Why this is not called parse file?
	if (!m_entries) return false;

	BuildMetadata();

	return true;
}

int LogEngine::ProcessRequestAndGetCount(uint8_t fLevel, uint8_t fThread, uint64_t fMinTime, uint64_t fMaxTime) {
	m_requestedEntries.clear();
	m_requestedEntries.reserve(m_entryCount);

	for (const LogMetaData& log : m_metadata) {
		if (fLevel != 0 && fLevel != log.level) continue;
		if (fThread != 0 && fThread != log.thread_id) continue;
		if (fMinTime != 0 && fMinTime > log.timestamp) continue;
		if (fMaxTime != 0 && fMaxTime < log.timestamp) continue;

		m_requestedEntries.push_back(log.entryPtr);
	}

	return (int)m_requestedEntries.size();
}

void LogEngine::BuildMetadata() {

	m_metadata.resize(m_entryCount);

	for (int i = 0; i < m_entryCount; i++) {
		LogMetaData& meta = m_metadata[i];
		LogEntry& raw = m_entries[i];

		meta.entryPtr = &raw;
		meta.timestamp = ParseTimeStamp(raw.date, raw.time);
		meta.level = MapLogLevel(raw.level);
		meta.thread_id = raw.thread_id[7] - '0';

		if (meta.level == 1) m_infoCount++;
		else if (meta.level == 2) m_debugCount++;
		else if (meta.level == 3) m_warningCount++;
		else if (meta.level == 4) m_errorCount++;
		else if (meta.level == 5) m_fatalCount++;

	}
}