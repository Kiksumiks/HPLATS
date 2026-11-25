#ifndef _PARSER_H_
#define _PARSER_H_

typedef struct {
    char* date;
    char* time;
    char* level;
    char* thread_id;
    char* message;
} LogEntry;

// Read and write file into dynamic array buffer.
// [in] filepath - path to log file
char* readFileIntoBuffer(const char* filepath);

// Reads file into buffer and creates dynamic array of LogEntry's.
// [out] out_count - Count of entries
// [in] file_buffer - Buffer that holds all bytes in file
LogEntry* load_log_file(int* out_count, char* file_buffer);

// Parses line for specific format. This "format" is just pointers pointing into filebuffer.
// [in] line - string to be parsed.
// [in / out] out_entry - Entry to be filled with parsed data.
int parse_log_line(char* line, LogEntry* out_entry);

// Frees dynamic entry list and file buffer.
// [in] entries - Pointer to our dynamic LogEntry array.
// [in] file_buffer - Pointer to our dynamic file buffer.
void free_log_entrys(LogEntry* entries, char* file_buffer);

#endif