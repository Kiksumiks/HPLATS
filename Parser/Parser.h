#ifndef _PARSER_H_
#define _PARSER_H_

typedef struct {
    char* date;
    char* time;
    char* level;
    char* thread_id;
    char* message;
} LogEntry;

// Read file into buffer.
// [in] filepath - path to log file
char* readFileIntoBuffer(const char* filepath);

// Reads file into buffer and creates dynamic array of LogEntry's.
// [out] out_count - Count of entries
// [in] file_buffer - Buffer that holds all bytes in file
LogEntry* load_log_file(int* out_count, char* file_buffer);

// Parses line for specific format.
// [in] line - string to be parsed.
// [out] out_entry - Entry which will be filled with parsed data.
int parse_log_line(char* line, LogEntry* out_entry);

// Free dynamic entry list and file buffer.
// [in] entries - Pointer to start of our dynamic entry array.
// [in] file_buffer - Pointer to dynamic file buffer.
void free_log_entrys(LogEntry* entries, char* file_buffer);

#endif