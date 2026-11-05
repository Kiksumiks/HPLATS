#ifndef _PARSER_H_
#define _PARSER_H_

typedef struct {
    char* date;
    char* time;
    char* level;
    char* thread_id;
    char* message;
} LogEntry;

// Reads file into buffer and creates dynamic array of LogEntry's.
// [in] filepath - path to log file
// [out] out_count - count of entries
// [out] file_buffer - dynamically allocated array for file content. Function allocates this using malloc so when calling this should be NULL.
LogEntry* load_log_file(const char* filepath, int* out_count, char* file_buffer);

// Parses line for specific format.
// [in] line - string to be parsed.
// [out] out_entry - Entry which will be filled with parsed data.
int parse_log_line(char* line, LogEntry* out_entry);

// Free dynamic entry list and file buffer.
// [in] entries - Pointer to start of our dynamic entry array.
// [in] file_buffer - Pointer to dynamic file buffer.
void free_log_entrys(LogEntry* entries, char* file_buffer);

#endif