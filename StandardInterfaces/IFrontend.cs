using Jsons;

namespace StandardInterfaces;

public interface IFrontend;

// these delegates should be used as a type to subscribe to events

public delegate Json PreprocessText(Json data);

public delegate Json ExtendLexemes(Json data);

public delegate Json PostprocessLexemes(Json data);

public delegate Json ExtendAstRules(Json data);

public delegate Json PostprocessAst(Json data);